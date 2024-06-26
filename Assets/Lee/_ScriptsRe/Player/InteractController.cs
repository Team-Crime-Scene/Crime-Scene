using UnityEngine;
using UnityEngine.InputSystem;

public class InteractController : MonoBehaviour
{
    [SerializeField] LayerMask interactableLayer; // 상호작용 가능한 레이어
    [SerializeField] Transform zoomPosition; // 줌 위치
    [SerializeField] float interactRange= 100;

    private bool isZoomed = false; // 줌 상태 여부
    public bool IsZoomed { get { return isZoomed; } }
    RaycastHit hit;
    Vector3 rayOrigin;
    Vector3 rayDirection;
    IRotatable rotatable;
    IZoomable zoomable;
    IReadable readable;
    IAnswerable answerable;
    private bool isreading = false;


    // 인터페이스
    // 마우스 움직임에 따라 바꿔줌 
    public void RotationCon()
    {
        if ( rotatable != null )
        {
            rotatable.Rotate();
        }
        else { return; }
    }


    // 커서가 안보일때만 회전되게함
    private void Update()
    {
        if ( Cursor.lockState == CursorLockMode.Locked )
            RotationCon();
    }

    // 상호작용 입력 처리 (마우스 좌클릭) 
    public void OnInteract( InputValue value )
    {
        rayOrigin = transform.position; // 레이 시작점은 플레이어 카메라 위치
        rayDirection = transform.forward; // 레이 방향은 플레이어 카메라의 정면 방향


        // 레이캐스트로 상호작용 가능한 대상 확인
        if ( Physics.Raycast(rayOrigin, rayDirection, out hit, interactRange, interactableLayer) )
        {
            rotatable = hit.transform.gameObject.GetComponent<IRotatable>();
            zoomable = hit.transform.gameObject.GetComponent<IZoomable>();
            readable = hit.transform.gameObject.GetComponent<IReadable>();
            answerable = hit.transform.gameObject.GetComponent<IAnswerable>();


            if ( zoomable != null && isZoomed == false )
            {
                zoomable.ZoomObject(zoomPosition);
                isZoomed = true;
            }
        }
    }

    // Q키 
    public void OnCancel( InputValue value )
    {
        if ( zoomable != null && isreading == false )
        {
            zoomable.UnzoomObject(zoomPosition);
            rotatable = null;
            zoomable = null;
            readable = null;
            answerable = null;

            isZoomed = false;
        }
    }

    // 인터페이스
    // 마우스커서가 있을때는 오브젝트가 안돌아가게해주고 있을땐 돌아가게함 
    // 마우스 좌클릭
    public void OnClick( InputValue value )
    {
        if ( hit.transform != null && readable == null && answerable == null )
        {
            if ( Cursor.lockState == CursorLockMode.None )
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
        }
    }
    // 인터페이스
    // 마우스를 보일때와 안보일때 컨트롤해줌 
    public void OnRotationCon( InputValue value )
    {
        if ( rotatable != null )
            rotatable.GetRotationInput(value);

    }
    // 읽기 인터페이스
    // R키를 누르면 읽기 ui가 나오게해줌
    public void OnRead( InputValue value )
    {
        if ( readable == null )
            return;

        isreading = !isreading;
        readable.Read();
    }

}
