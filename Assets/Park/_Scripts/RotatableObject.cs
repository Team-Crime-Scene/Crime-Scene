using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatableObject : InteractableObject , IDragHandler 
{
    [SerializeField] PopUpUI popUpUI;
    float rotateSpeed= 10;

    bool isInteract=false;

    void Awake()
    {
        popUpUI = Resources.Load<PopUpUI>("UI/RotatableUI"); //최적화 생각하면 지워야함
    }

    public override void Interact( PlayerController player )
    {
        base.Interact(player);
        Manager.UI.ShowPopUpUI(popUpUI); //
        isInteract = true;
        Cursor.visible = isInteract;
    }

    public void OnDrag( PointerEventData eventData )
    {
        if ( !isInteract ) return;

        float x = eventData.delta.x * Time.unscaledDeltaTime * rotateSpeed; //deltaTime => unscaledDeltaTime
        float y = eventData.delta.y * Time.unscaledDeltaTime * rotateSpeed;

        // transform.Rotate(0, -x, y, Space.World);
        transform.Rotate(Vector3.up, -x);
        transform.Rotate(Vector3.right, y);
    }

    public override void UnInteract( PlayerController player )
    {
        base.UnInteract( player );
        isInteract=false;
        Cursor.visible = isInteract;
    }
}
