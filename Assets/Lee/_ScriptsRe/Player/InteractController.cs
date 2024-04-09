using UnityEngine;
using UnityEngine.InputSystem;

public class InteractController : MonoBehaviour
{
    [SerializeField] LayerMask interactableLayer; // ��ȣ�ۿ� ������ ���̾�
    [SerializeField] Transform zoomPosition; // �� ��ġ
    [SerializeField] float interactRange= 100;

    private bool isZoomed = false; // �� ���� ����
    public bool IsZoomed { get { return isZoomed; } }
    RaycastHit hit;
    Vector3 rayOrigin;
    Vector3 rayDirection;
    IRotatable rotatable;
    IZoomable zoomable;
    IReadable readable;
    IAnswerable answerable;
    private bool isreading = false;


    // �������̽�
    // ���콺 �����ӿ� ���� �ٲ��� 
    public void RotationCon()
    {
        if ( rotatable != null )
        {
            rotatable.Rotate();
        }
        else { return; }
    }


    // Ŀ���� �Ⱥ��϶��� ȸ���ǰ���
    private void Update()
    {
        if ( Cursor.lockState == CursorLockMode.Locked )
            RotationCon();
    }

    // ��ȣ�ۿ� �Է� ó�� (���콺 ��Ŭ��) 
    public void OnInteract( InputValue value )
    {
        rayOrigin = transform.position; // ���� �������� �÷��̾� ī�޶� ��ġ
        rayDirection = transform.forward; // ���� ������ �÷��̾� ī�޶��� ���� ����


        // ����ĳ��Ʈ�� ��ȣ�ۿ� ������ ��� Ȯ��
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

    // QŰ 
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

    // �������̽�
    // ���콺Ŀ���� �������� ������Ʈ�� �ȵ��ư������ְ� ������ ���ư����� 
    // ���콺 ��Ŭ��
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
    // �������̽�
    // ���콺�� ���϶��� �Ⱥ��϶� ��Ʈ������ 
    public void OnRotationCon( InputValue value )
    {
        if ( rotatable != null )
            rotatable.GetRotationInput(value);

    }
    // �б� �������̽�
    // RŰ�� ������ �б� ui�� ����������
    public void OnRead( InputValue value )
    {
        if ( readable == null )
            return;

        isreading = !isreading;
        readable.Read();
    }

}
