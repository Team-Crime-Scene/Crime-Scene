using UnityEngine;
using UnityEngine.InputSystem;

public class RotInteractContorller : MonoBehaviour, IRotatable, IZoomable
{
    private Vector3 initialPosition; //초기위치값
    private Quaternion initialRotation; // 초기 회전값

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void Rotation()
    {
        transform.Rotate(initialRotation.x, 0, 0);
        transform.Rotate(0, initialRotation.y, 0);
    }
    public void RotationTrnas( InputValue Value )
    {
        Vector2 input = Value.Get<Vector2>();
        initialRotation.x = input.x;
        initialRotation.y = input.y;
    }

    public void UnzoomObject( Transform ZoomTrans )
    {
        transform.position = Vector3.Lerp(initialPosition, ZoomTrans.position, Time.deltaTime * 2f);
        transform.rotation = initialRotation;

        // 줌 해체시 커서 꺼짐
        Cursor.lockState = CursorLockMode.Locked;

    }

    public void ZoomObject( Transform ZoomTrans )
    {
        // 카메라와 대상 사이의 방향 벡터 계산하여 대상이 플레이어 카메라를 바라보도록 함
        Vector3 cameraToObject = transform.position - ZoomTrans.position;
        transform.rotation = Quaternion.LookRotation(cameraToObject);

        // 옵젝을 플레이어 앞으로 옮김
        transform.position = Vector3.Lerp(ZoomTrans.position, transform.position, Time.deltaTime * 2f);

        // 오브젝트를 가져왔을 때 커서보이게
        Cursor.lockState = CursorLockMode.None;

    }

   
}
