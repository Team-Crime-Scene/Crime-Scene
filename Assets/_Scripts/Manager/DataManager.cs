using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DataManager : Singleton<DataManager>
{
    private GameData gameData;
    public GameData GameData { get { return gameData; } }

#if UNITY_EDITOR
    private string path => Path.Combine(Application.dataPath, $"Resources/Data/SaveLoad");
#else
    private string path => Path.Combine(Application.persistentDataPath, $"Resources/Data/SaveLoad");
#endif

    public void NewData()
    {
        gameData = new GameData();
    }

    public void SaveData(int index = 0)
    {
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText($"{path}/{index}.txt", json);
    }

    public void LoadData(int index = 0)
    {
        if (File.Exists($"{path}/{index}.txt") == false)
        {
            NewData();
            return;
        }

        string json = File.ReadAllText($"{path}/{index}.txt");
        try
        {
            gameData = JsonUtility.FromJson<GameData>(json);
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"Load data fail : {ex.Message}");
            NewData();
        }
    }

    public bool ExistData(int index = 0)
    {
        return File.Exists($"{path}/{index}.txt");
    }

    public void SavePicture (GameObject Picture)
    {
        // picture찾는 로직
        gameData.tutorialData.Picture.Add(Picture);
        SaveData(0);
    }
    public void LoadPictuer()
    {
        for ( int i = 0; i < gameData.tutorialData.Picture.Count; i++ )
            Instantiate(gameData.tutorialData.Picture [i], gameData.tutorialData.Picture [i].gameObject.transform);

    }
    public void SaveAnswer( List<TMP_InputField> PlayerSubAnswers1 , List<TextMeshProUGUI> PlayerMultiAnswer , int score)
    {
        if ( Manager.Data.GameData.tutorialData.PlayerSubAnswers1 != null )
            Manager.Data.GameData.tutorialData.PlayerSubAnswers1.Clear();
        if(Manager.Data.GameData.tutorialData.PlayerMultiAnswer !=null)
            Manager.Data.GameData.tutorialData.PlayerMultiAnswer.Clear();

        // 주관식 저장
        for ( int i = 0; i < PlayerSubAnswers1.Count; i++ )
        {
            Manager.Data.GameData.tutorialData.PlayerSubAnswers1.Add(PlayerSubAnswers1 [i].text);
        }
        // 객관식 저장
        for ( int i = 0; i < PlayerMultiAnswer.Count; i++ )
        {
            Manager.Data.GameData.tutorialData.PlayerMultiAnswer.Add(PlayerMultiAnswer [i].GetComponentInParent<TMP_Dropdown>().value);
        }
        Manager.Data.GameData.tutorialData.tutorialScore = score;
        SaveData(0);
    }
    public void LoadAnswer( List<TMP_InputField> PlayerSubAnswers1, List<TextMeshProUGUI> PlayerMultiAnswer )
    {
        // 저장 불러오기
        for ( int i = 0; i < Manager.Data.GameData.tutorialData.PlayerSubAnswers1.Count; i++ )
            PlayerSubAnswers1 [i].text = Manager.Data.GameData.tutorialData.PlayerSubAnswers1 [i];
        for ( int i = 0; i < Manager.Data.GameData.tutorialData.PlayerMultiAnswer.Count; i++ )
            PlayerMultiAnswer [i].GetComponentInParent<TMP_Dropdown>().value = Manager.Data.GameData.tutorialData.PlayerMultiAnswer [i];
    }
    public void SaveLines( List<LineRenderer> lines )
    {
        for(int i = 0; i < lines.Count; i++ )
            Manager.Data.GameData.tutorialData.lines.Add(lines [i].gameObject);
            SaveData(0);
    }

    // parent =화이트보드이면될듯?
    public void LoadLines(Transform parent)
    {
        for ( int i = 0; i < Manager.Data.GameData.tutorialData.lines.Count; i++ )
            Instantiate(Manager.Data.GameData.tutorialData.lines [i], Manager.Data.GameData.tutorialData.lines [i].transform.position,
                Manager.Data.GameData.tutorialData.lines [i].transform.rotation, parent); ;
    }
}
