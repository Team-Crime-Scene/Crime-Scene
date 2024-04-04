using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RefatoringScene : BaseScene
{
    // ������ �����͵� 
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
        // ���� �ε�� �����͵��� �־���ߵ�
        player.position = Manager.Data.GameData.gameSceneData.playerPos;
        player.rotation = Manager.Data.GameData.gameSceneData.playerRot;
        PlayerSubAnswers.text = Manager.Data.GameData.gameSceneData.PlayerSubAnswers;
        PlayerMultiAnswer.text = Manager.Data.GameData.gameSceneData.PlayerMultiAnswer;
        Debug.Log(Manager.Data.GameData.gameSceneData.playerPos);

    }

    IEnumerator AutoSaveRutine()
    {
        while ( true )
        {
            //������ �ڵ�����
            yield return new WaitForSeconds(3f);
            Debug.Log("�ڵ� ����");
            Debug.Log(player.position);
            Manager.Data.GameData.gameSceneData.playerPos = player.position;
            Manager.Data.GameData.gameSceneData.playerRot = player.rotation;
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