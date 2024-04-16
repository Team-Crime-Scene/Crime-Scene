using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private GameData gameData;
    public GameData GameData { get { return gameData; } }

    [SerializeField] LineRenderer linePrefab;

#if UNITY_EDITOR
    private string path => Path.Combine(Application.dataPath, $"Resources/Data/SaveLoad");
#else
    private string path => Path.Combine(Application.persistentDataPath, $"Resources/Data/SaveLoad");
#endif

    public void NewData()
    {
        if ( ExistData() )
        {
            File.Delete($"{path}/0.txt");
        }
        gameData = new GameData();
    }

    public void SaveData( int index = 0 )
    {
        if ( Directory.Exists(path) == false )
        {
            Directory.CreateDirectory(path);
        }

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText($"{path}/{index}.txt", json);
    }

    public void LoadData( int index = 0 )
    {
        if ( File.Exists($"{path}/{index}.txt") == false )
        {
            NewData();
            return;
        }

        string json = File.ReadAllText($"{path}/{index}.txt");
        try
        {
            gameData = JsonUtility.FromJson<GameData>(json);
        }
        catch ( Exception ex )
        {
            Debug.LogWarning($"Load data fail : {ex.Message}");
            NewData();
        }
    }

    public bool ExistData( int index = 0 )
    {
        return File.Exists($"{path}/{index}.txt");
    }

    public void SaveAnswer( List<TMP_InputField> PlayerSubAnswers1, List<TextMeshProUGUI> PlayerMultiAnswer, int score )
    {
        if ( GameData.tutorialData.PlayerSubAnswers1 != null )
            GameData.tutorialData.PlayerSubAnswers1.Clear();
        if ( GameData.tutorialData.PlayerMultiAnswer != null )
            GameData.tutorialData.PlayerMultiAnswer.Clear();

        // 주관식 저장
        for ( int i = 0; i < PlayerSubAnswers1.Count; i++ )
        {
            GameData.tutorialData.PlayerSubAnswers1.Add(PlayerSubAnswers1 [i].text);
        }
        // 객관식 저장
        for ( int i = 0; i < PlayerMultiAnswer.Count; i++ )
        {
            GameData.tutorialData.PlayerMultiAnswer.Add(PlayerMultiAnswer [i].GetComponentInParent<TMP_Dropdown>().value);
        }
        GameData.tutorialData.tutorialScore = score;
        SaveData(0);
    }
    public void LoadAnswer( List<TMP_InputField> PlayerSubAnswers1, List<TextMeshProUGUI> PlayerMultiAnswer )
    {
        // 저장 불러오기
        for ( int i = 0; i < GameData.tutorialData.PlayerSubAnswers1.Count; i++ )
            PlayerSubAnswers1 [i].text = GameData.tutorialData.PlayerSubAnswers1 [i];
        for ( int i = 0; i < GameData.tutorialData.PlayerMultiAnswer.Count; i++ )
            PlayerMultiAnswer [i].GetComponentInParent<TMP_Dropdown>().value = GameData.tutorialData.PlayerMultiAnswer [i];
    }
    public void SaveLines( List<LineRenderer> lines )
    {
        for ( int i = 0; i < lines.Count; i++ )
        {
            LineData data = new LineData();
            data.color = lines [i].startColor;
            data.count = lines [i].positionCount;
            data.pos = new Vector3 [data.count];
            lines [i].GetPositions(data.pos);
            GameData.tutorialData.lineDatas.Add(data);
        }
        SaveData(0);
    }

    // parent =화이트보드이면될듯?
    public void LoadLines( EnhancedWhiteBoard whiteBoard )
    {
        for ( int i = 0; i < gameData.tutorialData.lineDatas.Count; i++ )
        {
            LineRenderer line = Instantiate(linePrefab, new Vector3(whiteBoard.transform.transform.position.x,
                whiteBoard.transform.position.y, whiteBoard.transform.position.z - 0.1f),
                whiteBoard.transform.rotation, whiteBoard.transform);
            //프리팹 건드지 마세요 ㅡ.ㅡ 미안...
            whiteBoard.AddLine(line);
            line.positionCount = gameData.tutorialData.lineDatas [i].count;
            line.SetPositions(gameData.tutorialData.lineDatas [i].pos);
            line.startColor = gameData.tutorialData.lineDatas [i].color;
            line.endColor = gameData.tutorialData.lineDatas [i].color;
        }
    }
    public void SaveAnswer1( TMP_InputField PlayerSubAnswers1, List<TMP_InputField> PlayerSubAnswers2, TMP_InputField PlayerSubAnswers3,
                        List<TextMeshProUGUI> PlayerMultiAnswer, TMP_InputField PlayerSubAnswers5, int score )
    {
        if ( GameData.chapter1Data.PlayerSubAnswers2 != null )
            GameData.chapter1Data.PlayerSubAnswers2.Clear();
        if ( GameData.chapter1Data.PlayerMultiAnswer != null )
            GameData.chapter1Data.PlayerMultiAnswer.Clear();

        // 주관식 저장

        gameData.chapter1Data.PlayerSubAnswers1 = PlayerSubAnswers1.text;
        for ( int i = 0; i < PlayerSubAnswers2.Count; i++ )
        {
            GameData.chapter1Data.PlayerSubAnswers2.Add(PlayerSubAnswers2 [i].text);
        }
        gameData.chapter1Data.PlayerSubAnswers3 = PlayerSubAnswers3.text;
        // 객관식 저장
        for ( int i = 0; i < PlayerMultiAnswer.Count; i++ )
        {
            GameData.chapter1Data.PlayerMultiAnswer.Add(PlayerMultiAnswer [i].GetComponentInParent<TMP_Dropdown>().value);
        }
        gameData.chapter1Data.PlayerSubAnswers5 = PlayerSubAnswers5.text;
        GameData.chapter1Data.Score = score;
        SaveData(0);
    }
    public void LoadAnswer1( TMP_InputField PlayerSubAnswers1, List<TMP_InputField> PlayerSubAnswers2, TMP_InputField PlayerSubAnswers3,
                        List<TextMeshProUGUI> PlayerMultiAnswer, TMP_InputField PlayerSubAnswers5 )
    {
        // 저장 불러오기
        PlayerSubAnswers1.text = gameData.chapter1Data.PlayerSubAnswers1;
        for ( int i = 0; i < GameData.chapter1Data.PlayerSubAnswers2.Count; i++ )
            PlayerSubAnswers2 [i].text = GameData.chapter1Data.PlayerSubAnswers2 [i];
        PlayerSubAnswers3.text = gameData.chapter1Data.PlayerSubAnswers3;
        for ( int i = 0; i < GameData.chapter1Data.PlayerMultiAnswer.Count; i++ )
            PlayerMultiAnswer [i].GetComponentInParent<TMP_Dropdown>().value = GameData.chapter1Data.PlayerMultiAnswer [i];
        PlayerSubAnswers5.text = gameData.chapter1Data.PlayerSubAnswers5;
    }
    public void SaveLines1( List<LineRenderer> lines )
    {
        for ( int i = 0; i < lines.Count; i++ )
        {
            LineData data = new LineData();
            data.color = lines [i].startColor;
            data.count = lines [i].positionCount;
            data.pos = new Vector3 [data.count];
            lines [i].GetPositions(data.pos);
            GameData.chapter1Data.lineDatas.Add(data);
        }
        SaveData(0);
    }

    // parent =화이트보드이면될듯?
    public void LoadLines1( EnhancedWhiteBoard whiteBoard )
    {
        for ( int i = 0; i < gameData.chapter1Data.lineDatas.Count; i++ )
        {
            LineRenderer line = Instantiate(linePrefab, new Vector3(whiteBoard.transform.transform.position.x,
                whiteBoard.transform.position.y, whiteBoard.transform.position.z - 0.1f),
                whiteBoard.transform.rotation, whiteBoard.transform);
            //프리팹 건드지 마세요 ㅡ.ㅡ 미안...
            whiteBoard.AddLine(line);
            line.positionCount = gameData.chapter1Data.lineDatas [i].count;
            line.SetPositions(gameData.chapter1Data.lineDatas [i].pos);
            line.startColor = gameData.chapter1Data.lineDatas [i].color;
            line.endColor = gameData.chapter1Data.lineDatas [i].color;
        }
    }
}
