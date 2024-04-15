using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatableObject : InteractableObject , IDragHandler
{
    [SerializeField] PopUpUI popUpUI;
    [SerializeField] float rotateSpeed = 10;
    [SerializeField] float smoothSpeed = 0.04f;
    float mouseX;
    float mouseY;


    bool isInteract=false;

    void Awake()
    {
        popUpUI = Resources.Load<PopUpUI>("UI/RotatableUI"); //최적화 생각하면 지워야함
    }

    public override void Interact( PlayerController player )
    {
        base.Interact(player);
        Manager.UI.ShowPopUpUI(popUpUI); 
        isInteract = true;
        Cursor.visible = isInteract;
    }


    public void OnDrag( PointerEventData eventData )
    {
        if (!isInteract ) return;
        mouseX += eventData.delta.x * Time.unscaledDeltaTime * rotateSpeed;
        mouseY += eventData.delta.y * Time.unscaledDeltaTime * rotateSpeed;
    }

    private void LateUpdate()
    {
        if ( !isInteract ) return;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, -mouseX, mouseY), smoothSpeed);
        //transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(-mouseY, mouseX,0f), smoothSpeed);
    }

    public override void UnInteract( PlayerController player )
    {
        base.UnInteract( player );
        isInteract=false;
        mouseX = 0;
        mouseY = 0;
    }
}

  