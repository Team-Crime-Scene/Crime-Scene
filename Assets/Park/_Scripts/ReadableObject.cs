using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ReadableObject : InteractableObject , IReadable
{
    [SerializeField] PopUpUI popUpUI;
    [SerializeField] PopUpUI readInfoPrefab; 
    [SerializeField] Texture2D readInfo;

    public override void Interact( PlayerController player )
    {
        base.Interact(player);
        Manager.UI.ShowPopUpUI(popUpUI);
        Cursor.visible = false;
    }

    public void Read()
    {
        Cursor.visible = true;
        if( readInfoPrefab != null)
        {
             Manager.UI.ShowPopUpUI(readInfoPrefab);
        }
        Manager.UI.CreatePopUpFromTexture(readInfo);
    }

    public override void UnInteract( PlayerController player )
    {
        base.UnInteract(player);
        Cursor.visible = true;
    }



}
