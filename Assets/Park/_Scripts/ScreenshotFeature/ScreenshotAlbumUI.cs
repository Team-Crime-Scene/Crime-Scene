using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotAlbumUI : MonoBehaviour
{
    //사용자의 Album UI 조작을 처리하고 ScreenshotSystem과 상호작용 하는 스크립트
    [SerializeField] ScreenshotSystem screenshotSystem;
    [SerializeField] public GameObject albumPanel;
    public bool isActive = false;
    [SerializeField] GameObject ScreenshotSlotUIPrefab; //스크린샷 슬롯 UI Prefab


    /***********************************************************************
    *                               Unity Events
    ***********************************************************************/
    private void Awake()
    {
        screenshotSystem = Camera.main.GetComponent<ScreenshotSystem>();
    }

    private void OnEnable()
    {

        InitAlbum();
        //TestLoadSprite();
    }

    /***********************************************************************
    *                              Methods
    ***********************************************************************/

    private void InitAlbum()
    {
        Debug.Log("앨범 초기화");
        for ( int i = 0; i < screenshotSystem.screenshots.Count; i++ )
        {
            
            // 1. 경로에서 PNG 불러오기, 
            // 2. PNG를 UI에 들어갈 수 있는 datatype으로 (Sprite) 변환
             // Extension.LoadSprite(screenshots [i]);

            //2.
            //3. 알맞은 위치에 변환한 이미지를 생성? 배치? Prefab으로 ?

            // GameObject albumImage = Instantiate(screenshots[i] , transform.parent.position, true);
        }
    }

    private void TestLoadSprite()
    {
       
    }

}
