using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RefatoringScene : BaseScene
{
    // 저장할 데이터들 
    [SerializeField] Transform player;
    [SerializeField] TMP_InputField PlayerSubAnswers;
    [SerializeField] TextMeshProUGUI PlayerMultiAnswer;

    private void Awake()
    {

    }
    public override IEnumerator LoadingRoutine()
    {
        Manager.Data.LoadData();
        yield return null;
        StartCoroutine(AutoSaveRutine());
        // 여기 로드된 데잍터들을 넣어줘야됨
        player.position = Manager.Data.GameData.gameSceneData.playerPos;
        player.rotation = Manager.Data.GameData.gameSceneData.playerRot;
        Debug.Log(Manager.Data.GameData.gameSceneData.playerPos);

    }

    IEnumerator AutoSaveRutine()
    {
        while ( true )
        {
            //데이터 자동저장
            yield return new WaitForSeconds(3f);
            Debug.Log("자동 저장");
            Debug.Log(player.position);
            Manager.Data.GameData.gameSceneData.playerPos = player.position;
            Manager.Data.GameData.gameSceneData.playerRot = player.rotation;
            Manager.Data.SaveData();
        }
    }


}
