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
    // private float prevTimeScale; // timeScale 0 ���� ����� VirtualCamera ��ȯ�� �ȵǴ� ������ �־ ���� 
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
            if ( topUI is ScreenshotAlbumUI )
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

    // Initate ���� �ʰ� Active on/off ��ľ��� Album UI�� PopUp Stack���� ���� �����ϱ� ���� ���� �޼���(...)
    public ScreenshotAlbumUI ShowAlbumUI( ScreenshotAlbumUI screenshotAlbumUI )
    {
        if ( popUpStack.Count > 0 )
        {
            PopUpUI topUI = popUpStack.Peek();
            topUI.gameObject.SetActive(false);
        }
        else
        {
            //popUpBlocker.gameObject.SetActive(true);
            // prevTimeScale = Time.timeScale;
            //Time.timeScale = 0f;
        }

        screenshotAlbumUI.Active();
        popUpStack.Push(screenshotAlbumUI);
        return screenshotAlbumUI;
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
        if ( ui is ScreenshotAlbumUI ) //ScreenshotAlbumUI �� destroy ���� �ʰ� Ű�� ���� ����ó��
        {
            Debug.Log("Close Album UI");
            ScreenshotAlbumUI screenshotAlbumUI = ( ScreenshotAlbumUI ) ui;
            screenshotAlbumUI.Active();
        }
        else
        {
            Destroy(ui.gameObject);
        }

        if ( popUpStack.Count > 0 )
        {
            PopUpUI topUI = popUpStack.Peek();
            if ( topUI is ScreenshotAlbumUI )
            {
                Debug.Log("Next UI is Album");
                ScreenshotAlbumUI screenshotAlbumUI = ( ScreenshotAlbumUI ) topUI;
               // screenshotAlbumUI.Active();
            }
            topUI.gameObject.SetActive(true);
        }
        else
        {
            //popUpBlocker.gameObject.SetActive(false);
            //Time.timeScale = prevTimeScale;
        }
    }

    public bool IsPopUpLastOne()
    {
        Debug.Log(popUpStack.Count);
        return ( popUpStack.Count==1 );
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
