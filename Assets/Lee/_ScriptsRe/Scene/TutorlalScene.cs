using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorlalScene : BaseScene
{
    [SerializeField] GameObject player;
    float AutoSaveGameTime = 300f;
    [SerializeField] Transform WhuteBoard;
    public override IEnumerator LoadingRoutine()
    {
        Manager.Game.PlayerFind();
        Manager.Data.LoadData();
        yield return null;
        Manager.Data.LoadPictuer();
        Manager.Data.LoadLines(WhuteBoard);
        player.transform.position = Manager.Data.GameData.tutorialData.playerPos;
        player.transform.rotation = Manager.Data.GameData.tutorialData.playerRot;
        Debug.Log(Manager.Data.GameData.tutorialData.playerPos);
        StartCoroutine(AutoSaveRutine());

    }
    IEnumerator AutoSaveRutine()
    {
        while ( true )
        {
            yield return new WaitForSeconds(AutoSaveGameTime);
            Debug.Log("자동 저장");
            Manager.Data.GameData.tutorialData.playerPos = player.transform.position;
            Manager.Data.GameData.tutorialData.playerRot = player.transform.rotation;
            Manager.Data.SaveData();
        }
    }

}
