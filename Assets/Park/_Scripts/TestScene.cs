using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestScene : BaseScene
{
    // ������ �����͵� 
    [SerializeField] GameObject player;
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_InputField PlayerSubAnswers;
    [SerializeField] TextMeshProUGUI PlayerMultiAnswer;

    [SerializeField] float AutoSaveGameTime =300f;

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
            //������ �ڵ�����
            yield return new WaitForSeconds(AutoSaveGameTime);
            Debug.Log("�ڵ� ����");
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
