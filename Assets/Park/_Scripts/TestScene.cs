using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestScene : BaseScene
{
    // 저장할 데이터들 
    [SerializeField] GameObject player;
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_InputField PlayerSubAnswers;
    [SerializeField] TextMeshProUGUI PlayerMultiAnswer;

    [SerializeField] float AutoSaveGameTime =3f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    public override IEnumerator LoadingRoutine()
    {
        Manager.Data.LoadData();
        yield return null;
        player.transform.position = Manager.Data.GameData.gameSceneData.playerPos;
        player.transform.rotation = Manager.Data.GameData.gameSceneData.playerRot;
        PlayerSubAnswers.text = Manager.Data.GameData.gameSceneData.PlayerSubAnswers;
        PlayerMultiAnswer.text = Manager.Data.GameData.gameSceneData.PlayerMultiAnswer;
        Debug.Log(Manager.Data.GameData.gameSceneData.playerPos);
        StartCoroutine(AutoSaveRutine());
    }

    IEnumerator AutoSaveRutine()
    {
        while ( true )
        {
            //데이터 자동저장
            yield return new WaitForSeconds(AutoSaveGameTime);
            Debug.Log("자동 저장");
            Manager.Data.GameData.gameSceneData.playerPos = player.transform.position;
            Manager.Data.GameData.gameSceneData.playerRot = player.transform.rotation;
            Manager.Data.GameData.gameSceneData.PlayerSubAnswers = PlayerSubAnswers.text;
            Manager.Data.GameData.gameSceneData.PlayerMultiAnswer = PlayerMultiAnswer.text;
            Manager.Data.SaveData();
        }
    }

    public void Submit()
    {
        PlayerSubAnswers.text = Manager.Data.GameData.gameSceneData.PlayerSubAnswers;
        PlayerMultiAnswer.text = Manager.Data.GameData.gameSceneData.PlayerMultiAnswer;
    }

}
