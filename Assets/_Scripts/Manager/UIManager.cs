using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] Canvas popUpCanvas;
    [SerializeField] Canvas windowCanvas;
    [SerializeField] Canvas inGameCanvas;

    [SerializeField]
    ReadableObjectUI readInfoPrefab;

    [SerializeField] Image popUpBlocker;
    [SerializeField] Button inGameBlocker;

    private Stack<PopUpUI> popUpStack = new Stack<PopUpUI>();
    // private float prevTimeScale; // timeScale 0 으로 만들시 VirtualCamera 전환이 안되는 문제가 있어서 꺼둠 
    private InGameUI curInGameUI;

    private void Start()
    {
        EnsureEventSystem();
    }

    public void EnsureEventSystem()
    {
        if ( EventSystem.current != null )
            return;

        EventSystem eventSystem = Resources.Load<EventSystem>("UI/EventSystem");
        Instantiate(eventSystem);
    }


    // PoPUpUI Methods //
    public T ShowPopUpUI<T>( T popUpUI ) where T : PopUpUI
    {
        if ( popUpStack.Count > 0 )
        {
            PopUpUI topUI = popUpStack.Peek();
            if ( topUI is ScreenshotAlbumUI || topUI is WhiteBoardUI)
            {

            }
            else
            {
                topUI.gameObject.SetActive(false);
            }
        }
        else
        {
            //popUpBlocker.gameObject.SetActive(true);
            // prevTimeScale = Time.timeScale;
            //Time.timeScale = 0f;
        }

        T ui = Instantiate(popUpUI, popUpCanvas.transform);
        popUpStack.Push(ui);
        return ui;
    }

    // Initate 하지 않고 Active on/off 방식쓰는 Album UI를 PopUp Stack으로 같이 관리하기 위한 전용 메서드(...)
    public ScreenshotAlbumUI ShowAlbumUI( ScreenshotAlbumUI screenshotAlbumUI )
    {
        if ( popUpStack.Count > 0 )
        {
            PopUpUI topUI = popUpStack.Peek();
            topUI.gameObject.SetActive(false);
        }

        screenshotAlbumUI.Active();
        popUpStack.Push(screenshotAlbumUI);
        return screenshotAlbumUI;
    }

    public WhiteBoardUI ShowWhiteBoardUI( WhiteBoardUI whiteBoardUI )
    {
        if ( popUpStack.Count > 0 )
        {
            PopUpUI topUI = popUpStack.Peek();
            topUI.gameObject.SetActive(false);
        }

        whiteBoardUI.Active();
        popUpStack.Push(whiteBoardUI);
        return whiteBoardUI;
    }

    public ReadableObjectUI CreatePopUpFromTexture(Texture2D texture2D )
    {
        ReadableObjectUI readableObjectUI = Instantiate(readInfoPrefab, popUpCanvas.transform);
        readableObjectUI.SetImage(texture2D);
        popUpStack.Push(readableObjectUI);
        return readableObjectUI;
    }


    public void ClosePopUpUI()
    {
        if ( popUpStack.Count == 0 ) return; 

        PopUpUI ui = popUpStack.Pop();
        if ( ui is ScreenshotAlbumUI ) //ScreenshotAlbumUI 만 destroy 하지 않고 키고 끄게 예외처리
        {
            ScreenshotAlbumUI screenshotAlbumUI = ( ScreenshotAlbumUI ) ui;
            screenshotAlbumUI.Active();
        }else if(ui is WhiteBoardUI )
        {
            WhiteBoardUI whiteBoardUI = ( WhiteBoardUI ) ui;
            whiteBoardUI.Active();
        }
        else
        {
            Destroy(ui.gameObject);
        }

        if ( popUpStack.Count > 0 )
        {
            PopUpUI topUI = popUpStack.Peek();
            topUI.gameObject.SetActive(true);
        }
    }

    public bool IsPopUpLastOne()
    {
        return ( popUpStack.Count==1 );
    }

    public bool IsPopUpZero()
    {
        return ( popUpStack.Count == 0 );
    }

    public void ClearPopUpUI()
    {
        while ( popUpStack.Count > 0 )
        {
            ClosePopUpUI();
        }
    }

    #region UnUsed


    // WindowUI Methods //

    public T ShowWindowUI<T>( T windowUI ) where T : WindowUI
    {
        return Instantiate(windowUI, windowCanvas.transform);
    }

    public void SelectWindowUI( WindowUI windowUI )
    {
        windowUI.transform.SetAsLastSibling();
    }

    public void CloseWindowUI( WindowUI windowUI )
    {
        Destroy(windowUI.gameObject);
    }

    public void ClearWindowUI()
    {
        for ( int i = 0; i < windowCanvas.transform.childCount; i++ )
        {
            Destroy(windowCanvas.transform.GetChild(i).gameObject);
        }
    }

    // InGameUI Methods //

    public T ShowInGameUI<T>( T inGameUI ) where T : InGameUI
    {
        if ( curInGameUI != null )
        {
            Destroy(curInGameUI.gameObject);
        }

        T ui = Instantiate(inGameUI, inGameCanvas.transform);
        curInGameUI = ui;
        inGameBlocker.gameObject.SetActive(true);
        return ui;
    }

    public void CloseInGameUI()
    {
        if ( curInGameUI == null )
            return;

        inGameBlocker.gameObject.SetActive(false);
        Destroy(curInGameUI.gameObject);
        curInGameUI = null;
    }
    #endregion
}
