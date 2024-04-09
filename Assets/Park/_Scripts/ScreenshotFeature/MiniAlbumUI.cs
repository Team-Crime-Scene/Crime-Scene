using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniAlbumUI : MonoBehaviour
{
    [SerializeField] MiniSlotUI ScreenshotSlotUIPrefab; //스크린샷 슬롯 UI Prefab

    public List<MiniSlotUI> screenshotSlots;

    [SerializeField] GameObject albumPanel;
    [SerializeField] RectTransform albumGrid;


    private float height;

    bool isActive = false;
    bool isInit = false;
    /***********************************************************************
    *                               Unity Events
    ***********************************************************************/
    protected void Awake()
    {
        screenshotSlots = new List<MiniSlotUI>();
        InitAlbumUISlots();
    }



    /***********************************************************************
    *                              Methods
    ***********************************************************************/

    //그리드 내에 ScreenshotSlotUI를 동적으로 생성

    // ToDo 슬롯 생성/삭제시 ScrollView Content(album Grid)의 Height를 동적으로 증감해주는 것
    private void InitAlbumUISlots()
    {
        Debug.Log("앨범 초기화");
        int count = ScreenshotAlbum.Instance.Screenshots.Count;
        for ( int i = 0; i < count; i++ )
        {
            MiniSlotUI slot = Instantiate(ScreenshotSlotUIPrefab);
            RectTransform rect = slot.GetComponent<RectTransform>();
            slot.screenshot = ScreenshotAlbum.Instance.Screenshots [i];
            rect.SetParent(albumGrid);
            rect.localScale = Vector3.one;
            screenshotSlots.Add(slot);
        }
        SetGridSize(count);
    }

    public void UpdateAlbumUISlots()
    {
        Debug.Log("앨범 업데이트");
        int count = ScreenshotAlbum.Instance.Screenshots.Count;
        MiniSlotUI slot = Instantiate(ScreenshotSlotUIPrefab);
        RectTransform rect = slot.GetComponent<RectTransform>();
        slot.screenshot = ScreenshotAlbum.Instance.Screenshots [count - 1];
        rect.SetParent(albumGrid);
        rect.localScale = Vector3.one;
        screenshotSlots.Add(slot);
        SetGridSize(count);
    }

    private void SetGridSize( int count )
    {
        height = ( count ) * 110 + 100;
        albumGrid.sizeDelta = new Vector2(albumGrid.sizeDelta.x, height);
    }

    public void Active()
    {
        isActive = !isActive;
        albumPanel.SetActive(isActive);

        if ( !isInit ) // 최초 실행시에만 초기화
        {
            isInit = true;
            InitAlbumUISlots();
        }
    }
}
