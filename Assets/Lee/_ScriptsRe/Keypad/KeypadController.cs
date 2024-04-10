using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadController : MonoBehaviour, IInteractable
{
    [SerializeField] CinemachineVirtualCamera vCam;
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
