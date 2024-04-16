using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class TitleScene : BaseScene
{
    [SerializeField] Button continueButton;
    [SerializeField] AudioClip titleBGM;

    float defaultVolume;
    private void Start()
    {
        bool exist = Manager.Data.ExistData();
        defaultVolume = Manager.Sound.BGMVolme;
        Manager.Sound.PlayBGM(titleBGM);  
        // 불러오기 없으면 선택 안됨
        continueButton.interactable = exist;
    }
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
    public void ContinueGame()
    {
        Manager.Scene.LoadScene("LobbyScene");
    }

    public void StartGame()
    {
        Manager.Data.NewData();
        Manager.Scene.LoadScene("LobbyScene");
    }

    public void ButtonMute()
    {
        Manager.Sound.BGMVolme = 0;
    }

    public void ButtonSoundON()
    {
        Manager.Sound.BGMVolme = defaultVolume;
    }

    public void ButtonEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
