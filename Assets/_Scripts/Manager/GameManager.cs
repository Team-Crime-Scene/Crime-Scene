using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    //상호작용 여부에 따라 플레이어 조작 제한
    //활성화된 UI 창에 따라 플레이어 조작 제한
    [SerializeField] PlayerController player;
    [SerializeField] ScreenshotSystem screenshotSystem;
    [SerializeField] IInteractable interactObject;

    void Start()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        player = gameObject.GetComponent<PlayerController>();
        screenshotSystem = Camera.main.GetComponent<ScreenshotSystem>();
    }

    public void Interaction(IInteractable interactable)
    {
        Debug.Log("Manager Interact");
        player.isInteract = true;
     
        interactObject= interactable;
        interactObject?.Interact(player);
    }

    public void OnCancel()
    {
       if(!Manager.UI.IsPopUpLeft()) //원 조건 palyer.isInteract;
       {
            Debug.Log("Manager Cancel");
            player.isInteract = false;
            interactObject?.UnInteract(player);
       }
       Manager.UI.ClosePopUpUI();
    }

    void OnPause(InputValue inputValue){
        if ( Manager.UI.IsPopUpLeft() ) return;
        if ( inputValue.isPressed )
        {

        }
    }

    public void OnScreenshot(InputValue inputValue)
    {
        if ( inputValue.isPressed )
        {
            screenshotSystem.isTakeScreenshot=true;
        }
    }

    public void OnRead( InputValue inputValue )
    {
        if( inputValue.isPressed )
        {
            if(interactObject is IReadable )
            {
                IReadable readable = ( IReadable ) interactObject;
                readable.Read();
            }
            //ReadableObject readable = interactObject. 상호작용중인 대상의 Read UI PopUP 하도록 요청
        }
    }

    public void OnAlbum( InputValue inputValue )
    {
        if ( inputValue.isPressed )
        {
            screenshotSystem.OpenAlbum();
            player.isInteract = screenshotSystem.IsOpend();
        }
    }
}
