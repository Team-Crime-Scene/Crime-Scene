using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// 챕터 만들때마다 만들자
public class GameManager : Singleton<GameManager>
{
    //상호작용 여부에 따라 플레이어 조작 제한
    //활성화된 UI 창에 따라 플레이어 조작 제한
    [SerializeField] PlayerController player;
    [SerializeField] ScreenshotSystem screenshotSystem;
    [SerializeField] IInteractable interactObject;

    bool isChating;

    public void ChangeIsChatTrue()
    {
        isChating = true;
        Debug.Log($"비활성화 : {isChating}");
    }
    public void ChangeIsChatFalse()
    {
        isChating = false;
        Debug.Log($"활성화 : {isChating}");
    }

    void Start()
    {
        // 타이틀 씬에서 못찾아서 따로 함수 만들어서 플레이어 있을때 붙여줌(게임 씬에서 실험 할때 잠시 씀)
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        player = gameObject.GetComponent<PlayerController>();
        screenshotSystem = Camera.main.GetComponent<ScreenshotSystem>();
    }
    public void PlayerFind()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        player = gameObject.GetComponent<PlayerController>();
        screenshotSystem = Camera.main.GetComponent<ScreenshotSystem>();
    }

    public void Interaction( IInteractable interactable )
    {
        player.isInteract = true;

        interactObject = interactable;
        interactObject?.Interact(player);
    }

    public void OnCancel()
    {
        if ( isChating == true) return;
        if ( Manager.UI.IsPopUpLastOne() ) //원 조건 palyer.isInteract;
        {
            Debug.Log("Manager Cancel");
            player.isInteract = false;
            interactObject?.UnInteract(player);
        }
        Manager.UI.ClosePopUpUI();
    }

    void OnPause( InputValue inputValue )
    {
        if ( Manager.UI.IsPopUpLastOne() ) return;
        if ( inputValue.isPressed )
        {
            // 여기서 Pause UI Instantiate
        }
    }

    public void OnScreenshot( InputValue inputValue )
    {
        if ( isChating == true ) return;
        if ( player == null ) return;

        if ( inputValue.isPressed )
        {
            screenshotSystem.isTakeScreenshot = true;
        }
    }

    public void OnRead( InputValue inputValue )
    {
        if ( inputValue.isPressed )
        {
            if ( interactObject is IReadable )
            {
                IReadable readable = ( IReadable )interactObject;
                readable.Read();
            }
        }
    }

    public void OnAlbum( InputValue inputValue )
    {
        if ( player == null ) return;

        if ( inputValue.isPressed )
        {
            screenshotSystem.OpenAlbum();
            player.isInteract = screenshotSystem.IsOpend();
        }
    }
}
