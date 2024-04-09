using UnityEngine;
using UnityEngine.InputSystem;

public class RotInteractContorller : MonoBehaviour, IRotatable, IZoomable
{
    private Vector3 initialPosition; //�ʱ���ġ��
    private Quaternion initialRotation; // �ʱ� ȸ����

    private Vector2 rotateDir;

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void Rotate()
    {
        transform.Rotate(initialRotation.x, 0, 0);
        transform.Rotate(0, initialRotation.y, 0);
    }
    public void GetRotationInput( InputValue Value )
    {
        Vector2 input = Value.Get<Vector2>();
        rotateDir.x = input.x;
        rotateDir.y = input.y;
    }

    public void UnzoomObject( Transform ZoomTrans )
    {
        transform.position = Vector3.Lerp(initialPosition, ZoomTrans.position, Time.deltaTime * 2f);
        transform.rotation = initialRotation;

        // �� ��ü�� Ŀ�� ����
        Cursor.lockState = CursorLockMode.Locked;

    }

    public void ZoomObject( Transform ZoomTrans )
    {
        // ī�޶�� ��� ������ ���� ���� ����Ͽ� ����� �÷��̾� ī�޶� �ٶ󺸵��� ��
        Vector3 cameraToObject = transform.position - ZoomTrans.position;
        transform.rotation = Quaternion.LookRotation(cameraToObject);

        // ������ �÷��̾� ������ �ű�
        transform.position = Vector3.Lerp(ZoomTrans.position, transform.position, Time.deltaTime * 2f);

        // ������Ʈ�� �������� �� Ŀ�����̰�
        Cursor.lockState = CursorLockMode.None;
    }
   
}
