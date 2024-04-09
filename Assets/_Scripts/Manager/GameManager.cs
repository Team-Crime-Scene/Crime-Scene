using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

// é�� ���鶧���� ������
public class GameManager : Singleton<GameManager>
{
    //��ȣ�ۿ� ���ο� ���� �÷��̾� ���� ����
    //Ȱ��ȭ�� UI â�� ���� �÷��̾� ���� ����
    [SerializeField] PlayerController player;
    [SerializeField] ScreenshotSystem screenshotSystem;
    [SerializeField] IInteractable interactObject;

    bool isChating;

    public void ChangeIsChatTrue()
    {
        isChating = true;
        Debug.Log($"��Ȱ��ȭ : {isChating}");
    }
    public void ChangeIsChatFalse()
    {
        isChating = false;
        Debug.Log($"Ȱ��ȭ : {isChating}");
    }

    void Start()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        player = gameObject.GetComponent<PlayerController>();
        screenshotSystem = Camera.main.GetComponent<ScreenshotSystem>();
    }

    public void Interaction( IInteractable interactable )
    {
        Debug.Log("Manager Interact");
        player.isInteract = true;

        interactObject = interactable;
        interactObject?.Interact(player);
    }

    public void OnCancel()
    {
        if ( isChating == true) return;
        Debug.Log($"����� : {isChating}");
        if ( Manager.UI.IsPopUpLastOne() ) //�� ���� palyer.isInteract;
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
            // ���⼭ Pause UI Instantiate
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
