using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractController : MonoBehaviour
{
    [SerializeField] Transform playerCamera; // 플레이어 카메라
    [SerializeField] LayerMask interactableLayer; // 상호작용 가능한 레이어
    [SerializeField] Transform zoomPosition; // 줌 위치

    private bool isZoomed = false; // 줌 상태 여부
    public bool IsZoomed { get { return isZoomed; } }
    private Quaternion initialRotation; // 초기 회전값
    private Vector3 initialPosition; // 초기 위치값
    RaycastHit hit;
    Vector3 rayOrigin;
    Vector3 rayDirection;


    // 대상을 줌 상태로 변경
    /*    private void ZoomObject( Transform objTransform )
        {
            interactrotation = objTransform.rotation;
            initialRotation = objTransform.rotation; // 초기 회전값 저장
            initialPosition = objTransform.position; // 초기 위치값 저장

            // 카메라와 대상 사이의 방향 벡터 계산하여 대상이 플레이어 카메라를 바라보도록 함
            Vector3 cameraToObject = objTransform.position - playerCamera.position;
            objTransform.rotation = Quaternion.LookRotation(cameraToObject);

            // 대상을 줌 위치로 이동시킴
            objTransform.position = Vector3.Lerp(zoomPosition.position, objTransform.position, Time.deltaTime * 2f);

            // 오브젝트를 가져왔을때 커서보이게
            Cursor.lockState = CursorLockMode.None;


            isZoomed = true; // 줌 상태로 변경
        }

        // 대상을 줌 상태 해제
        private void UnzoomObject( Transform objTransform )
        {
            // 대상을 초기 위치로 이동시킴
            objTransform.position = Vector3.Lerp(initialPosition, zoomPosition.position, Time.deltaTime * 2f);
            objTransform.rotation = initialRotation; // 대상의 회전을 초기 회전값으로 설정

            // 줌 해체시 커서 꺼짐
            Cursor.lockState = CursorLockMode.Locked;

            // 해제시 다시 쿼터니언을 생성해주어 null 상태로 만듬
            interactrotation = new Quaternion();
            isZoomed = false; // 줌 상태 해제
        }

        // 밑에까지 다 인터페이스 만드셈
        // 오브젝트의 쿼터니언
        Quaternion interactrotation;






        // 인터페이스
        // 마우스 움직임에 따라 바꿔줌
        public void RotationCon()
        {
            if ( hit.transform != null )
            {
                hit.transform.Rotate(interactrotation.x, 0, 0);
                hit.transform.Rotate(0, interactrotation.y, 0);
            }
        }
        // 커서가 안보일때만 회전되게함
        private void Update()
        {
            if ( Cursor.lockState == CursorLockMode.Locked )
                RotationCon();
        }
    */

    private void Start()
    {
        initialRotation = Quaternion.identity; // 초기 회전값을 초기화
    }

    // 상호작용 입력 처리
    public void OnInteract( InputValue value )
    {
        rayOrigin = playerCamera.position; // 레이 시작점은 플레이어 카메라 위치
        rayDirection = playerCamera.forward; // 레이 방향은 플레이어 카메라의 정면 방향


        // 레이캐스트로 상호작용 가능한 대상 확인
        if ( Physics.Raycast(rayOrigin, rayDirection, out hit, 100, interactableLayer) )
        {
            IRotatable rotatable = hit.transform.gameObject.GetComponent<IRotatable>();
            IZoomable zoomable = hit.transform.gameObject.GetComponent<IZoomable>();
            IReadable readable = hit.transform.gameObject.GetComponent<IReadable>();

            if(zoomable != null)
            {
                zoomable.ZoomObject(zoomPosition);
            }
        }
    }
    public void OnCancel( InputValue value )
    {
        //UnzoomObject(hit.transform); // 대상을 줌 해제
    }
    // 인터페이스
    // 마우스커서가 있을때는 오브젝트가 안돌아가게해주고 있을땐 돌아가게함
    public void OnClick( InputValue value )
    {
        /*if ( hit.transform != null )
        {
            if ( Cursor.lockState == CursorLockMode.None )
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
        }*/
    }
    // 인터페이스
    // 마우스를 보일때와 안보일때 컨트롤해줌 
    public void OnRotationCon( InputValue value )
    {
        /*  if ( gameObject.GetComponent<CamaraController>().enabled == false )
          {
              Vector2 input = value.Get<Vector2>();

              interactrotation.x = input.x;
              interactrotation.y = input.y;
          }*/

    }
    // 읽기 인터페이스
    // R키를 누르면 읽기 ui가 나오게해줌
    public void OnRead( InputValue value )
    {
        /*        Canvas readKey = gameObject.GetComponentInChildren<Canvas>(true);
                if ( readKey.enabled == false )
                    readKey.enabled = true;
                else
                    readKey.enabled = false;
        */
    }



}
