using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniAlbumUI : MonoBehaviour
{
    [SerializeField] MiniSlotUI ScreenshotSlotUIPrefab; //스크린샷 슬롯 UI Prefab

    public List<MiniSlotUI> screenshotSlots;

    [SerializeField] GameObject albumPanel;
    [SerializeField] RectTransform albumGrid;



    ScreenshotAlbum album;
    private float height;

    bool isActive = false;
    bool isInit = false;
    /***********************************************************************
    *                               Unity Events
    ***********************************************************************/
    protected void Awake()
    {
        screenshotSlots = new List<MiniSlotUI>();
    }

    private void Start()
    {
        album = ScreenshotAlbum.Instance;
        album.OnScreenshotDeleted += OnScreenshotDeleted;
    }


    /***********************************************************************
    *                              Methods
    ***********************************************************************/

    public void InitAlbumUISlots()
    {
        Debug.Log("미니 앨범 초기화");
        int count = ScreenshotAlbum.Instance.Screenshots.Count;
        for ( int i = 0; i < count; i++ )
        {
            MiniSlotUI slot = Instantiate(ScreenshotSlotUIPrefab);
            RectTransform rect = slot.GetComponent<RectTransform>();
            slot.Screenshot = ScreenshotAlbum.Instance.Screenshots [i];
            rect.SetParent(albumGrid, false);
            rect.localScale = Vector3.one;
            screenshotSlots.Add(slot);
        }
        SetGridSize(count);
        isInit = true;
    }

    public void UpdateAlbumUISlots()
    {
        Debug.Log("미니 앨범 업데이트");
        int count = ScreenshotAlbum.Instance.Screenshots.Count;
        MiniSlotUI slot = Instantiate(ScreenshotSlotUIPrefab);
        RectTransform rect = slot.GetComponent<RectTransform>();
        slot.Screenshot = ScreenshotAlbum.Instance.Screenshots [count - 1];
        rect.SetParent(albumGrid, false);
        rect.localScale = Vector3.one;
        screenshotSlots.Add(slot);
        SetGridSize(count);
    }

    public void OnScreenshotDeleted( Screenshot screenshot )
    {
        Debug.Log($"이거 삭제한다고함 => {screenshot.Data.path}");
        MiniSlotUI slotUI = screenshotSlots.Find(s => s.Screenshot.Data.path == screenshot.Data.path); //그냥 Screenshot 객체만 가지고 찾았더니 가장먼저 찾은 젤 첫번째 객체 반환함;;
        if ( slotUI != null )
        {
            Debug.Log("삭제할 슬롯 찾았음");
            screenshotSlots.Remove(slotUI);
            slotUI.Delete();
        }
        SetGridSize(ScreenshotAlbum.Instance.Screenshots.Count);
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
            InitAlbumUISlots();
        }
    }
}
