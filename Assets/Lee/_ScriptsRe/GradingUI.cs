using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GradingUI : MonoBehaviour
{
    [SerializeField] List<GameObject> aRank;
    [SerializeField] List<GameObject> bRank;
    [SerializeField] List<GameObject> cRank;
    [SerializeField] List<GameObject> DRank;
    private void Awake()
    {
        for ( int i = 0; i < aRank.Count; i++ )
        {
            if ( Manager.Data.GameData.tutorialData.tutorialScore == 0 )
            {
                DRank [i].SetActive(true);
            }
            else if ( Manager.Data.GameData.tutorialData.tutorialScore == 1 )
            {
                cRank [i].SetActive(true);
            }
            else if ( Manager.Data.GameData.tutorialData.tutorialScore == 2 )
            {
                bRank [i].SetActive(true);
            }
            else if ( Manager.Data.GameData.tutorialData.tutorialScore == 3 )
            {
                aRank [i].SetActive(true);
            }
        }

       
    }
    public void OKButton()
    {
            Manager.Scene.LoadScene("LobbyScene");
    }
}
