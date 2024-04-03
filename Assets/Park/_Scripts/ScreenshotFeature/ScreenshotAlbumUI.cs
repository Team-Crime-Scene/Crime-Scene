using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotAlbumUI : MonoBehaviour
{
    // View
    //사용자의 Album UI 조작을 처리하고 ScreenshotSystem과 상호작용 하는 스크립트
    [SerializeField] ScreenshotSystem screenshotSystem; //이것도 직접참조 원래는 하면 안됨... 나중에 Event로 처리 
    [SerializeField] Image selectedScreenshot; //선택된 스크린샷 이미지
    [SerializeField] ScreenshotSlotUI ScreenshotSlotUIPrefab; //스크린샷 슬롯 UI Prefab
    [SerializeField] GameObject albumPanel;
    [SerializeField] Transform albumGrid;
   
    bool isActive = false;

    /***********************************************************************
    *                               Unity Events
    ***********************************************************************/
    private void Awake()
    {
        screenshotSystem = Camera.main.GetComponent<ScreenshotSystem>();
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
            rect.SetParent(albumGrid);
            rect.localScale = Vector3.one;
        }
    }

    private void UpdateAlbum()
    {
        Debug.Log("앨범 업데이트");
        InitAlbum();
        // screenshotSystem의 list를 순회 후  
        // slot의 추가 삭제 처리를 통해 동기화
    }

    public void Active()
    {
        isActive = !isActive;
        albumPanel.SetActive(isActive);

        UpdateAlbum();
    }
}
