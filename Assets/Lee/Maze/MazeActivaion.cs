using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeActivaion : InteractableObject
{
    [SerializeField] CinemachineVirtualCamera mazeCamera;

    public override void Interact( PlayerController player )
    {
        mazeCamera.Priority = 100;
    }

    public override void UnInteract( PlayerController player )
    {
        mazeCamera.Priority = 0;

    }

}
