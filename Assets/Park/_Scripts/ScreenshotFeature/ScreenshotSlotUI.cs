using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotSlotUI : MonoBehaviour
{
    //Album UI안의 각 스크린샷 슬롯

    // 슬롯의 인덱스 관리, 슬롯의 Screenshot 이미지, 하이라이트

    [SerializeField] Image image;
    [SerializeField] public string path;
    [SerializeField] public int index;

    public ScreenshotAlbumUI albumUI;
    
    private void Start()
    {
        image.sprite = Extension.LoadSprite(path);
    }

    public void Remove()
    {
        Destroy(gameObject);
    }

    public void OnClick()
    {
        albumUI.curSlotIndex = index;
        albumUI.selectedScreenshotImage.sprite = Extension.LoadSprite(path);
    }
}
