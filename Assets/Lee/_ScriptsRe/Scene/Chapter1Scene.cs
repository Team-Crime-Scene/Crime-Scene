using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter1Scene : BaseScene
{
    [SerializeField] GameObject player;
    float AutoSaveGameTime = 300;
    [SerializeField] EnhancedWhiteBoard WhiteBoard;
    [SerializeField] LineRenderer linePrefab;

    [SerializeField] AudioClip TutorialBGM;

    private void Start()
    {
#if UNITY_EDITOR
        StartCoroutine(LoadingRoutine());//for Debuging at this Scene
#endif
    }
    public override IEnumerator LoadingRoutine()
    {
        Manager.Game.InitGameManager();
        Manager.Data.LoadData();
        yield return null;
        player = GameObject.FindGameObjectWithTag("Player");
        WhiteBoard = FindAnyObjectByType<EnhancedWhiteBoard>();
        Manager.Sound.PlayBGM(TutorialBGM);
        Manager.Data.LoadLines(WhiteBoard);
        //player.transform.position = Manager.Data.GameData.chapter1Data.playerPos;
        //.transform.rotation = Manager.Data.GameData.chapter1Data.playerRot;
        StartCoroutine(AutoSaveRutine());
    }
    IEnumerator AutoSaveRutine()
    {
        while ( true )
        {
            yield return new WaitForSeconds(AutoSaveGameTime);
            Debug.Log("자동 저장");
            Manager.Data.GameData.chapter1Data.playerPos = player.transform.position;
            Manager.Data.GameData.chapter1Data.playerRot = player.transform.rotation;
            Manager.Data.SaveData();
        }
    }
}
