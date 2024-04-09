using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour , IInteractable
{
    protected Vector3 initialPosition; //�ʱ���ġ��
    protected Quaternion initialRotation; // �ʱ� ȸ����

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }


    public virtual void Interact( PlayerController player )
    {
        transform.position = player.ZoomedPos.position;
        transform.rotation = player.ZoomedPos.rotation;
    }

    public virtual void UnInteract( PlayerController player )
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        Manager.UI.ClosePopUpUI();
    }

}
