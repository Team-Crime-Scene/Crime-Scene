using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookedPanel : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] ScreenshotAlbumUI albumUI;


    public void OnEnable()
    {
        UpdateImage();
    }
    private void UpdateImage()
    {
        image.sprite = Extension.LoadSprite(albumUI.curSlot.screenshot.Data.path);
    }

    public void OnClickButtonNext()
    {
        int currentIndex = albumUI.screenshotSlots.IndexOf(albumUI.curSlot);
        int nextIndex = ( currentIndex + 1 ) % albumUI.screenshotSlots.Count;
        albumUI.curSlot = albumUI.screenshotSlots [nextIndex];
        albumUI.UpdateSelectedImage();
        UpdateImage();
    }

    public void OnClickButtonPrev()
    {
        int currentIndex = albumUI.screenshotSlots.IndexOf(albumUI.curSlot);
        int prevIndex = ( currentIndex - 1 + albumUI.screenshotSlots.Count ) % albumUI.screenshotSlots.Count;
        albumUI.curSlot = albumUI.screenshotSlots [prevIndex];
        albumUI.UpdateSelectedImage();
        UpdateImage();
    }


}
