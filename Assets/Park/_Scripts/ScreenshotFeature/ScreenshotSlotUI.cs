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
    [SerializeField] Image markedImage;
    [SerializeField] public int index;

    [SerializeField] public Screenshot screenshot;
    // Todo 일일히 path와 index를 받는것이 아니라 Screenshot 객체를 참조하도록 바꿀것

    public ScreenshotAlbumUI albumUI;
    
    private void Start()
    {
        image.sprite = Extension.LoadSprite(screenshot.Data.path);
        UpdateMarking();
    }


    public void OnClick()
    {
        albumUI.curIndex = index;
        albumUI.selectedScreenshotImage.sprite = Extension.LoadSprite(screenshot.Data.path);
    }

    public void UpdateMarking()
    {
        markedImage.enabled = screenshot.Data.isBookmarked;
    }
    public void Remove()
    {
        Destroy(gameObject);
    }

}
