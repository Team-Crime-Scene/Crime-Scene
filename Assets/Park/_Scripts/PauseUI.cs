using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : PopUpUI
{
    PlayerController player;
    float vol;
    private void Start()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        if(gameObject != null )
        {
            player = gameObject?.GetComponent<PlayerController>();
            player.isInteract = true;
        }
        Time.timeScale = 0f;
        vol = Manager.Sound.BGMVolme;
        Manager.Sound.BGMVolme = vol * 0.25f;
    }

    public void ButtonPasue()
    {
        Manager.UI.ClosePopUpUI();
    }

    public void ButtonExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    private void OnDestroy()
    {
        player.isInteract = false;
        Manager.Sound.BGMVolme = vol;
        Time.timeScale = 1f;
    }
}

