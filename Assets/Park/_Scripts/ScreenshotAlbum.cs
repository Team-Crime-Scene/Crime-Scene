using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotAlbum : MonoBehaviour
{
    [SerializeField] ScreenshotSystem screenshotSystem;

    List<string> screenshots;
    [SerializeField] int maxAlbumCount = 100;

    /***********************************************************************
    *                               Unity Events
    ***********************************************************************/
    private void Awake()
    {
        screenshotSystem= Camera.main.GetComponent<ScreenshotSystem>();
    }

    private void OnEnable()
    {
        screenshots = screenshotSystem.screenshots;

    }

}
