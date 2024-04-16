using System.Collections;
using UnityEngine;

public class TutorlalScene : BaseScene
{
    [SerializeField] GameObject player;
    float AutoSaveGameTime = 300f;
    [SerializeField] EnhancedWhiteBoard WhiteBoard;
    [SerializeField] LineRenderer linePrefab;

    private void Start()
    {
        //StartCoroutine(LoadingRoutine());//forDebug 
    }

    public override IEnumerator LoadingRoutine()
    {
        Manager.Game.InitGameManager();
        Manager.Data.LoadData();
        yield return null;
        player = GameObject.FindGameObjectWithTag("Player");
        WhiteBoard = FindAnyObjectByType<EnhancedWhiteBoard>();
        Manager.Data.LoadLines(WhiteBoard);
        player.transform.position = Manager.Data.GameData.tutorialData.playerPos;
        player.transform.rotation = Manager.Data.GameData.tutorialData.playerRot;
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
