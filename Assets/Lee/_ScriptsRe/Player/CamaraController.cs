using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// ��ȣ�ۿ붧�� ������� �ʴ´�.
public class CamaraController : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera mainCamera;
    CinemachineTransposer transposer;

    enum PlayerPosture
    {
        Standing, SitDown, Crouching
    }
    PlayerPosture playerPosture = PlayerPosture.Standing;

    // ĳ���� ũ�⿡ ���� ���� ���¿� ���ִ� ���°� �޶� ���� ����
    Vector3 StandingPos = new Vector3(0, 1f, 0);
    Vector3 SitDownPos = new Vector3(0, 0.5f, 0);
    Vector3 CrouchingPos = new Vector3(0, 0.25f, 0);

    // �� ��ȣ�ۿ뿡�� ���ߵ�
    // �÷��̾� ������ Ȯ��
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
        // FOv�� �̿��Ͽ� Ȯ�밡 �Ȱ�ó�� ���̰���
        if ( mainCamera.m_Lens.FieldOfView == 60 )
            mainCamera.m_Lens.FieldOfView = 40;
        else
            mainCamera.m_Lens.FieldOfView = 60;
    }

    // ���������� ���� ���� ��ȭ
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

    // ���콺

    // ī�޶��� ���Ʒ��� ������ ���Ʒ��� ��
    [SerializeField] Transform cameraRoot;
    [SerializeField] float mouseSensitivity;
    // �÷��̾��� �����̼��� ���� ī�޶� ���� ���ư���
    [SerializeField] Transform player;
    private Vector2 inputDir;
    private float xRotation;

    private void OnEnable()
    {
        // ���콺�� ����� ������ ���ڸ��� �ְ�����(Ŀ���� ������Ե�)
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDisable()
    {
        // ����� ����� ������
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
