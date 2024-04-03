using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenshotAlbumUI : MonoBehaviour
{
    // View
    //사용자의 Album UI 조작을 처리하고 ScreenshotSystem과 상호작용 하는 스크립트
    [SerializeField] ScreenshotSystem screenshotSystem; //이것도 직접참조 원래는 하면 안됨... 나중에 Event로 처리 
    [SerializeField] public Image selectedScreenshotImage; //선택된 스크린샷 이미지
    [SerializeField] ScreenshotSlotUI ScreenshotSlotUIPrefab; //스크린샷 슬롯 UI Prefab
    List<ScreenshotSlotUI> screenshotSlots;
    public int curSlotIndex = 0;
    [SerializeField] GameObject albumPanel;
    [SerializeField] Transform albumGrid;
   
    
    
    bool isActive = false;
    bool isInit = false;
    /***********************************************************************
    *                               Unity Events
    ***********************************************************************/
    private void Awake()
    {
        screenshotSystem = Camera.main.GetComponent<ScreenshotSystem>();
        screenshotSlots = new List<ScreenshotSlotUI>();
    }


  
    /***********************************************************************
    *                              Methods
    ***********************************************************************/

    //그리드 내에 ScreenshotSlotUI를 동적으로 생성
    private void InitAlbum()
    {
        Debug.Log("앨범 초기화");
        for ( int i = 0; i < screenshotSystem.screenshots.Count; i++ )
        {
            ScreenshotSlotUI slot = Instantiate(ScreenshotSlotUIPrefab);
            RectTransform rect = slot.GetComponent<RectTransform>();
            slot.path = screenshotSystem.screenshots [i].Data.path;
            slot.index = i;
            slot.albumUI = this;    
            rect.SetParent(albumGrid);
            rect.localScale = Vector3.one;
            screenshotSlots.Add(slot);
        }
    }

    public void UpdateAlbum()
    {
        Debug.Log("앨범 업데이트");
        ScreenshotSlotUI slot = Instantiate(ScreenshotSlotUIPrefab);
        RectTransform rect = slot.GetComponent<RectTransform>();
        slot.path = screenshotSystem.screenshots [screenshotSystem.screenshots.Count-1].Data.path;
        slot.index = screenshotSystem.screenshots.Count - 1;
        slot.albumUI = this;
        rect.SetParent(albumGrid);
        rect.localScale = Vector3.one;
        screenshotSlots.Add(slot);
    }

    private void DeleteFromAlbum()
    {
        screenshotSlots [curSlotIndex].Remove();
        screenshotSlots.RemoveAt(curSlotIndex);
        screenshotSystem.Delete(curSlotIndex);
    }

    public void Active()
    {
        isActive = !isActive;
        albumPanel.SetActive(isActive);

        if ( !isInit )
        {
            isInit = true;
            InitAlbum();
        }
            
    }

    /***********************************************************************
    *                              OnClick Events
    ***********************************************************************/
    
    public void ButtonDelete()
    {
        DeleteFromAlbum();
    }

    public void ButtonLook()
    {
        //현재 선택되있는 사진을 확대
    }

    public void ButtonMarking()
    {
        screenshotSystem.screenshots [curSlotIndex].Data.isBookmarked = !screenshotSystem.screenshots [curSlotIndex].Data.isBookmarked;
    }

}
