using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookedPanel : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] ScreenshotAlbumUI albumUI;

    int maxIndex;
    int curIndex;

    public void OnEnable()
    {
        curIndex = albumUI.curIndex;
        maxIndex = albumUI.screenshotSlots.Count;
        UpdateImage();
    }

    public void OnDisable()
    {
       albumUI.curIndex = curIndex;
    }

    private void UpdateImage()
    {
        image = albumUI.selectedScreenshotImage;
    }

    public void OnClickButtonNext()
    {
        if(curIndex < maxIndex)
        {
            curIndex++;
        }
        else
        {
            curIndex = 0;
        }
        UpdateImage();
    }

    public void OnClickButtonPrev()
    {
        if ( curIndex != 0 )
        {
            curIndex--;
        }
        else
        {
            curIndex = albumUI.screenshotSlots.Count -1;
        }
        UpdateImage();
    }

}
