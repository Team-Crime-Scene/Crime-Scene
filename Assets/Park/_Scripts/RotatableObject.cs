using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatableObject : InteractableObject , IPointerMoveHandler 
{
    [SerializeField] PopUpUI popUpUI;
    float rotateSpeed= 10;

    void Awake()
    {
        popUpUI = Resources.Load<PopUpUI>("UI/RotatableUI"); //최적화 생각하면 지워야함
    }

    public override void Interact( PlayerController player )
    {
        base.Interact(player);
        Manager.UI.ShowPopUpUI(popUpUI); //
        Cursor.visible = false;
    }

    public void OnPointerMove( PointerEventData eventData )
    {
        float x = eventData.delta.x * Time.unscaledDeltaTime * rotateSpeed; //deltaTime => unscaledDeltaTime
        float y = eventData.delta.y * Time.unscaledDeltaTime * rotateSpeed;

        transform.Rotate(0, -x, y, Space.World);
    }

    public override void UnInteract( PlayerController player )
    {
        base.UnInteract( player );
        Cursor.visible = true;
    }
}
