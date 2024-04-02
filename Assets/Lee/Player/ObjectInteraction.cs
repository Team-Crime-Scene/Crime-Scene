using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] PlayerInput control;
    [SerializeField] Transform playerCamera; // 플레이어 카메라
    [SerializeField] LayerMask interactableLayer; // 상호작용 가능한 레이어
    [SerializeField] Transform zoomPosition; // 줌 위치
    [SerializeField] Image aim;
    [SerializeField] Image background;

    private bool isZoomed = false; // 줌 상태 여부
    private Quaternion initialRotation; // 초기 회전값
    private Vector3 initialPosition; // 초기 위치값
    RaycastHit hit;
    Vector3 rayOrigin;
    Vector3 rayDirection;
    private void Start()
    {
        initialRotation = Quaternion.identity; // 초기 회전값을 초기화
    }

    // 상호작용 입력 처리
    public void OnInteraction( InputValue value )
    {
        rayOrigin = playerCamera.position; // 레이 시작점은 플레이어 카메라 위치
        rayDirection = playerCamera.forward; // 레이 방향은 플레이어 카메라의 정면 방향

        // 레이캐스트로 상호작용 가능한 대상 확인
        if ( Physics.Raycast(rayOrigin, rayDirection, out hit, 100, interactableLayer) )
        {
            // 줌되어 있지 않은 상태라면
            if ( !isZoomed )
            {
                ZoomObject(hit.transform); // 대상을 줌
                PlayerIdle(); // 플레이어 움직임 뺏기
            }

        }
    }
    public void OnCancel( InputValue value )
    {
        UnzoomObject(hit.transform); // 대상을 줌 해제
        ResumeMovement(); // 움직임 활성화
    }

    // 대상을 줌 상태로 변경
    private void ZoomObject( Transform objTransform )
    {
        initialRotation = objTransform.rotation; // 초기 회전값 저장
        initialPosition = objTransform.position; // 초기 위치값 저장

        // 카메라와 대상 사이의 방향 벡터 계산하여 대상이 플레이어 카메라를 바라보도록 함
        Vector3 cameraToObject = objTransform.position - playerCamera.position;
        objTransform.rotation = Quaternion.LookRotation(cameraToObject);

        // 대상을 줌 위치로 이동시킴
        objTransform.position = Vector3.Lerp(zoomPosition.position, objTransform.position, Time.deltaTime * 2f);

        // 오브젝트를 가져왔을때 커서보이게
        Cursor.lockState = CursorLockMode.None;

        // 줌 상태일때 마우스로 통해 카메라를 움직이는걸 멈춤
        gameObject.GetComponent<PlayerCameraControl>().enabled = false;

        // 줌 상태일때 Aim이 사라짐
        aim.enabled = false;

        // ui 백그라운드를 생성
        background.enabled = true;

        isZoomed = true; // 줌 상태로 변경
    }

    // 대상을 줌 상태 해제
    private void UnzoomObject( Transform objTransform )
    {
        // 대상을 초기 위치로 이동시킴
        objTransform.position = Vector3.Lerp(initialPosition, zoomPosition.position, Time.deltaTime * 2f);
        objTransform.rotation = initialRotation; // 대상의 회전을 초기 회전값으로 설정

        // 줌 해체시 커서 꺼짐
        Cursor.lockState = CursorLockMode .Locked;

        // 줌 해제시 마우스로 다시 카메라 움직이게 함
        gameObject.GetComponent<PlayerCameraControl>().enabled = true;

        // aim를 다시 생성
        aim.enabled = true;

        // 백그라운드 없애기
        background.enabled = false;  


        isZoomed = false; // 줌 상태 해제
    }

    private void PlayerIdle()
    {
        control.enabled = false;
    }
    private void ResumeMovement()
    {
        control.enabled = true;
    }
}
