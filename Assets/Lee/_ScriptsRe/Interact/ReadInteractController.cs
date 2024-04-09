using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadInteractController : MonoBehaviour, IReadable, IZoomable
{
    private Vector3 initialPosition; //�ʱ���ġ��
    private Quaternion initialRotation; // �ʱ� ȸ����
    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void Read()
    {
        Canvas readKey = gameObject.GetComponentInChildren<Canvas>(true);
        if ( readKey.enabled == false )
        {
            readKey.enabled = true;
        }
        else
        {
            readKey.enabled = false;
        }
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

    public void Interact( PlayerController player )
    {
        throw new System.NotImplementedException();
    }

    public void UnInteract( PlayerController player )
    {
        throw new System.NotImplementedException();
    }
}
