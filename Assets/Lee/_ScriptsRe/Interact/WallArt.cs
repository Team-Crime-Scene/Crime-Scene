using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallArt : InteractableObject
{
    public override void UnInteract( PlayerController player )
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        Manager.UI.ClosePopUpUI();
    }

}
