using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatableObject : InteractableObject , IPointerMoveHandler 
{
    float rotateSpeed= 10;

    public override void Interact( PlayerController player )
    {
        base.Interact(player);
        Cursor.visible = false;
    }

    public void OnPointerMove( PointerEventData eventData )
    {
        float x = eventData.delta.x * Time.deltaTime * rotateSpeed;
        float y = eventData.delta.y * Time.deltaTime * rotateSpeed;

        transform.Rotate(0, -x, y, Space.World);
    }

    public override void UnInteract( PlayerController player )
    {
        base.UnInteract( player );
        Cursor.visible = true;
    }
}
