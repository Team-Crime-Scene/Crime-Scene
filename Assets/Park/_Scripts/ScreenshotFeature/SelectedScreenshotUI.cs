using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedScreenshotUI : MonoBehaviour
{
    [SerializeField] Image image;
    // ScreenshotAlbumUI���� �������� ������ ũ�� �����ִ� ��ũ��Ʈ

    public void UpdateSelected(ScreenshotData screenshotData)
    {
        image.sprite = Extension.LoadSprite(screenshotData.path);
    }
}
