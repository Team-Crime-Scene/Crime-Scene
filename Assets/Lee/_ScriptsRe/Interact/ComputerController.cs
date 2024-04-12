using Cinemachine;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerController : MonoBehaviour,IInteractable
{
    // 컴퓨터에 줌되는 카메라
    [SerializeField] CinemachineVirtualCamera vCam;
    // 컴퓨터에 들어가는 ui
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