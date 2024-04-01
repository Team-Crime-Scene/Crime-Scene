using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraControll : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera zoomCamera;
    public void OnZoom( InputValue value )
    {
        Zoom();
    }
    public void Zoom()
    {
        if ( zoomCamera.Priority == 10 )
            zoomCamera.Priority = 12;
        else
            zoomCamera.Priority = 10;
    }
}
