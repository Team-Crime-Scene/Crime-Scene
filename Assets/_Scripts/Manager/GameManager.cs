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
        player.isInteract = true;
     
        interactObject= interactable;
        interactObject?.Interact(player);
    }

    public void OnCancel()
    {
        if(player.isInteract)
        {
            Debug.Log("Manager Cancel");
            player.isInteract = false;
            interactObject?.UnInteract(player);
           // Manager.UI.ClearPopUpUI();
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
