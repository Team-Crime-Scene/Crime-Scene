using Cinemachine;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerController : MonoBehaviour,IInteractable
{
    // ��ǻ�Ϳ� �ܵǴ� ī�޶�
    [SerializeField] CinemachineVirtualCamera vCam;
    // ��ǻ�Ϳ� ���� ui
    [SerializeField] PopUpUI popUpUI;

    public void Interact( PlayerController player )
    {
        vCam.Priority = 100;
        Manager.UI.ShowPopUpUI(popUpUI);
    }

   

    public void UnInteract( PlayerController player )
    {
        vCam.Priority = 0;
    }

}