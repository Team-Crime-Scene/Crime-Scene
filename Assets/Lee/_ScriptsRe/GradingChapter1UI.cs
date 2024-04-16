using System.Collections.Generic;
using UnityEngine;

public class GradingChapter1UI : MonoBehaviour
{
    [SerializeField] List<GameObject> aRank;
    [SerializeField] List<GameObject> bRank;
    [SerializeField] List<GameObject> cRank;
    [SerializeField] List<GameObject> DRank;
    private void Awake()
    {
        for ( int i = 0; i < aRank.Count; i++ )
        {
            if ( Manager.Data.GameData.tutorialData.tutorialScore <= 1 )
            {
                DRank [i].SetActive(true);
            }
            else if ( Manager.Data.GameData.tutorialData.tutorialScore <= 3 )
            {
                cRank [i].SetActive(true);
            }
            else if ( Manager.Data.GameData.tutorialData.tutorialScore <= 4 )
            {
                bRank [i].SetActive(true);
            }
            else if ( Manager.Data.GameData.tutorialData.tutorialScore == 5 )
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
