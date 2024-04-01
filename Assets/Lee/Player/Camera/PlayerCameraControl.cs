using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraControl : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainCamera;

    int sitDownCount=0;
    // 캐릭터 크기에 따라 앉은 상태와 서있는 상태가 달라 질수 있음
    Vector3 SitDown1State = new Vector3(0, 0.5f, 0);
    Vector3 SitUpState = new Vector3(0, 1, 0);
    Vector3 SitDown2State = new Vector3(0, 0.25f, 0);
    private void OnZoom( InputValue value )
    {
        Zoom();
    }
    private void Zoom()
    {
        if ( mainCamera.m_Lens.FieldOfView == 60 )
            mainCamera.m_Lens.FieldOfView = 40;
        else
            mainCamera.m_Lens.FieldOfView = 60;
    }
    private void OnSitDown( InputValue value )
    {
        CinemachineTransposer transposer = mainCamera.GetCinemachineComponent<CinemachineTransposer>();

        if ( sitDownCount == 0 )
        {
            transposer.m_FollowOffset = SitDown1State;
            sitDownCount = 1;
        }
        else if ( sitDownCount == 1 ) 
        {
            transposer.m_FollowOffset = SitDown2State;
            sitDownCount = 2;
        }
        else if(sitDownCount == 2 )
        {
            transposer.m_FollowOffset = SitUpState;
            sitDownCount = 0;
        }
    }
    // 마우스

    // 카메라의 위아래를 돌려서 위아래를 봄
    [SerializeField] Transform cameraRoot;
    [SerializeField] float mouseSensitivity;
    // 플레이어의 로테이션을 돌려 카메라도 같이 돌아가함
    [SerializeField] Transform player;
    private Vector2 inputDir;
    private float xRotation;

    private void OnEnable()
    {
        // 마우스가 가운데에 잡혀서 그자리에 있게해줌
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDisable()
    {
        // 가운데에 잡던걸 놓아줌
        Cursor.lockState = CursorLockMode.None;
    }
    private void Update()
    {

        xRotation -= inputDir.y * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        
        player.transform.Rotate(Vector3.up, inputDir.x * mouseSensitivity * Time.deltaTime);

        transform.Rotate(Vector3.up, mouseSensitivity * inputDir.x * Time.deltaTime);
        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    private void OnLook( InputValue value )
    {
        inputDir = value.Get<Vector2>();
    }

}
