using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotSlotUI : MonoBehaviour
{
    //Album UI���� �� ��ũ���� ����

    // ������ �ε��� ����, ������ Screenshot �̹���, ���̶���Ʈi

    [SerializeField] Image image;
    [SerializeField] Image markedImage;

    [SerializeField] public Screenshot screenshot;

    public ScreenshotAlbumUI albumUI;
    
    private void Start()
    {
        image.sprite = Extension.LoadSprite(screenshot.Data.path);
        markedImage.enabled = screenshot.Data.isBookmarked;
    }


    public void OnClick()
    {
        albumUI.curSlot = this;
        albumUI.UpdateSelectedImage();
    }

    public void UpdateMarking()
    {
        screenshot.Data.isBookmarked = !screenshot.Data.isBookmarked;
        markedImage.enabled = screenshot.Data.isBookmarked;
    }
    public void Delete()
    {
        ScreenshotAlbum.Instance.Delete(screenshot);
        Destroy(gameObject);
    }

}
