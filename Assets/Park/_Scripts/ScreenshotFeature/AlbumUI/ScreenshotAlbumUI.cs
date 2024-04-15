using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenshotAlbumUI : PopUpUI
{
    // View
    //사용자의 Album UI 조작을 처리하고 ScreenshotAlbum과 상호작용 하는 스크립트
    [SerializeField] Image selectedScreenshotImage; //선택된 스크린샷 이미지
    [SerializeField] ScreenshotSlotUI ScreenshotSlotUIPrefab; //스크린샷 슬롯 UI Prefab
  
    public List<ScreenshotSlotUI> screenshotSlots;
    public ScreenshotSlotUI curSlot;

    [SerializeField] GameObject albumPanel;
    [SerializeField] PopUpUI lookedPanelUI;
    [SerializeField] RectTransform albumGrid;


    private float height;

    bool isActive = false;
    bool isInit = false;
    /***********************************************************************
    *                               Unity Events
    ***********************************************************************/
    protected override void Awake()
    {
        base.Awake();
        screenshotSlots = new List<ScreenshotSlotUI>();
        GetUI<Button>("BTN_Look").onClick.AddListener(ButtonLook);
        //InitAlbumUISlots();
    }


  
    /***********************************************************************
    *                              Methods
    ***********************************************************************/

    public void InitAlbumUISlots()
    {
        Debug.Log("Init AlbumUI");
        int count = ScreenshotAlbum.Instance.Screenshots.Count;
        if ( count == 0 ) return;

        for ( int i = 0; i < count; i++ )
        {
            ScreenshotSlotUI slot = Instantiate(ScreenshotSlotUIPrefab);
            RectTransform rect = slot.GetComponent<RectTransform>();
            slot.Screenshot = ScreenshotAlbum.Instance.Screenshots[i];
            slot.albumUI = this;    
            rect.SetParent(albumGrid);
            rect.localScale = Vector3.one;
            screenshotSlots.Add(slot);
            curSlot = slot;
        }
        SetGridSize(count);
        selectedScreenshotImage.sprite = Extension.LoadSprite(curSlot.Screenshot.Data.path);
        isInit = true;
    }

    public void UpdateAlbumUISlots()
    {
        Debug.Log("Album Slot Update");
        int count = ScreenshotAlbum.Instance.Screenshots.Count;
        ScreenshotSlotUI slot = Instantiate(ScreenshotSlotUIPrefab);
        RectTransform rect = slot.GetComponent<RectTransform>();
        slot.Screenshot = ScreenshotAlbum.Instance.Screenshots [count - 1];
        slot.albumUI = this;
        rect.SetParent(albumGrid);
        rect.localScale = Vector3.one;
        screenshotSlots.Add(slot);
        SetGridSize(count);
    }

    private void SetGridSize(int count )
    {
        height = (count / 3) * 110 + 100;
        albumGrid.sizeDelta = new Vector2(albumGrid.sizeDelta.x, height);
    }

    public void UpdateSelectedImage()
    {
        selectedScreenshotImage.sprite = Extension.LoadSprite(curSlot.Screenshot.Data.path);
    }

    private void DeleteFromAlbum()
    {
       screenshotSlots.Remove(curSlot);
       curSlot.Delete();
       SetGridSize(screenshotSlots.Count);
       curSlot = screenshotSlots [screenshotSlots.Count - 1];
       UpdateSelectedImage();
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

    public bool IsActive()
    {
        return isActive;
    }

    public bool IsInit()
    {
        return isInit;
    }

    /***********************************************************************
    *                              OnClick Events
    ***********************************************************************/
    
    public void ButtonDelete()
    {
        //확인팝업 묻는거 추가해야함
        DeleteFromAlbum();
    }

    public void ButtonLook()
    {
        Manager.UI.ShowPopUpUI(lookedPanelUI);
    }

    public void ButtonMarking()
    {
        curSlot.UpdateMarking();
    }

}
