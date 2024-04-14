using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScene : BaseScene
{
    [SerializeField] TextMeshProUGUI tutorialRank;
    [SerializeField] TextMeshProUGUI tutorialScore;
    public override IEnumerator LoadingRoutine()
    {
        Manager.Data.LoadData();
        tutorialScore.text = $"{Manager.Data.GameData.tutorialData.tutorialScore}/4 ";
        if( Manager.Data.GameData.tutorialData.tutorialScore/ 4 < 1)

        yield return null;
    }

    public void Rnak(int score)
    {
        
    }
    public void Tutorial()
    {
        Manager.Scene.LoadScene("LeeTutorialScene");
    }

}
