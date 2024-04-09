using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadableObjectUI : PopUpUI
{
    [SerializeField] Image readInfo;
    protected override void Awake()
    {
        base.Awake();
    }

    public void SetImage(Texture2D texture2D)
    {
        readInfo.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
        readInfo.SetNativeSize();
    }

}
