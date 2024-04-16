using System.Collections;
using UnityEngine;

public class TutorlalScene : BaseScene
{
    [SerializeField] GameObject player;
    float AutoSaveGameTime = 300f;
    [SerializeField] EnhancedWhiteBoard WhiteBoard;
    [SerializeField] LineRenderer linePrefab;

    [SerializeField] AudioClip TutorialBGM;

    private void Start()
    {
        StartCoroutine(LoadingRoutine());//forDebug 
    }

    public override IEnumerator LoadingRoutine()
    {
        Manager.Game.InitGameManager();
        Manager.Data.LoadData();
        yield return null;
        player = GameObject.FindGameObjectWithTag("Player");
        WhiteBoard = FindAnyObjectByType<EnhancedWhiteBoard>();
        Manager.Data.LoadLines(WhiteBoard);
        Manager.Sound.PlayBGM(TutorialBGM);
        Vector3 playerPos = Manager.Data.GameData.tutorialData.playerPos;
        if ( playerPos == Vector3.zero )
        {
            playerPos = new Vector3(0, 1, 0);
        }
        player.transform.position = playerPos;
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
