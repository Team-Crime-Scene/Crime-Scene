using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedScreenshotUI : MonoBehaviour
{
    [SerializeField] Image image;
    // ScreenshotAlbumUI에서 선택중인 사진을 크게 보여주는 스크립트

    public void UpdateSelected(ScreenshotData screenshotData)
    {
        image.sprite = Extension.LoadSprite(screenshotData.path);
    }
}
