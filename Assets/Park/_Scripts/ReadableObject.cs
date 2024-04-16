using UnityEngine;

public class ReadableObject : InteractableObject, IReadable
{
    [SerializeField] PopUpUI popUpUI;
    [SerializeField] PopUpUI readInfoPrefab;
    [SerializeField] Texture2D readInfo;
    [SerializeField] bool isAttachedToWall = false;

    public override void Interact( PlayerController player )
    {
        transform.rotation = player.ReadPos.rotation;
        if ( !isAttachedToWall )
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180f, transform.eulerAngles.z); //바닥에 놓여있던 물체의 경우 정면을 바라보게 회전
        }
        transform.position = player.ReadPos.position;
        Manager.UI.ShowPopUpUI(popUpUI);
        Cursor.visible = false;

    }

    public void Read()
    {
        Cursor.visible = true;
        if ( readInfoPrefab != null )
        {
            Manager.UI.ShowPopUpUI(readInfoPrefab);
        }
        if ( readInfo == null ) return;
        Manager.UI.CreatePopUpFromTexture(readInfo);
    }

    public override void UnInteract( PlayerController player )
    {
        base.UnInteract(player);
        Cursor.visible = true;
    }
}
