using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyScene : BaseScene
{
    [SerializeField] TextMeshProUGUI tutorialRank;
    [SerializeField] TextMeshProUGUI tutorialScore;
    [SerializeField] GameObject unlockChampter1;
    [SerializeField] GameObject notExam1;
    [SerializeField] GameObject image1;
    [SerializeField] List<GameObject> activate1;

    [SerializeField] AudioClip LobbyBGM;
    public override IEnumerator LoadingRoutine()
    {
        Manager.Data.LoadData();
        tutorialScore.text = $"{Manager.Data.GameData.tutorialData.tutorialScore}/3 ";
        TutorialRnak(Manager.Data.GameData.tutorialData.tutorialScore);
        Manager.Sound.PlayBGM(LobbyBGM);
        yield return null;

    }

    public void TutorialRnak( int score )
    {
        if ( score < 2 )
        {
            tutorialRank.text = "C";
        }
        else if ( score < 3 )
        {
            tutorialRank.text = "B";
        }
        else if ( score == 3 )
        {
            tutorialRank.text = "A";
        }
        if ( score >= 2 )
        {
            Debug.Log("µé¾î¿È");
            unlockChampter1.SetActive(false);
            notExam1.SetActive(false);
            image1.SetActive(false);
            for ( int i = 0; i < activate1.Count; i++ )
            {
                activate1 [i].SetActive(true);
            }
        }
    }
    public void Tutorial()
    {
        Manager.Scene.LoadScene("TutorialScene");
    }

    public void Chapter1()
    {
        Manager.Scene.LoadScene("Chapter1");
    }

}
