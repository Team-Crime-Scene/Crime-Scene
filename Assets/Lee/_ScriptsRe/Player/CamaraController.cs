using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// 상호작용때는 사용하지 않는다.
public class CamaraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera mainCamera;
    CinemachineTransposer transposer;

    enum PlayerPosture
    {
        Standing, SitDown, Crouching
    }
    PlayerPosture playerPosture = PlayerPosture.Standing;

    // 캐릭터 크기에 따라 앉은 상태와 서있는 상태가 달라 질수 있음
    Vector3 StandingPos = new Vector3(0, 1f, 0);
    Vector3 SitDownPos = new Vector3(0, 0.5f, 0);
    Vector3 CrouchingPos = new Vector3(0, 0.25f, 0);

    // 얜 상호작용에도 들어가야됨
    // 플레이어 조작중 확대
    private void Awake()
    {
        transposer = mainCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    private void OnZoom( InputValue value )
    {
        Zoom();
    }
    private void Zoom()
    {
        // FOv를 이용하여 확대가 된거처럼 보이게함
        if ( mainCamera.m_Lens.FieldOfView == 60 )
            mainCamera.m_Lens.FieldOfView = 40;
        else
            mainCamera.m_Lens.FieldOfView = 60;
    }

    // 누를때마다 앉은 상태 변화
    private void OnSitDown( InputValue value )
    {
        switch ( playerPosture )
        {
            case PlayerPosture.Standing:
                transposer.m_FollowOffset = SitDownPos;
                playerPosture = PlayerPosture.SitDown;
                break;
            case PlayerPosture.SitDown:
                transposer.m_FollowOffset = CrouchingPos;
                playerPosture = PlayerPosture.Crouching;
                break;
            case PlayerPosture.Crouching:
                transposer.m_FollowOffset = StandingPos;
                playerPosture = PlayerPosture.Standing;
                break;
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
        // 마우스가 가운데에 잡혀서 그자리에 있게해줌(커서가 사라지게됨)
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
