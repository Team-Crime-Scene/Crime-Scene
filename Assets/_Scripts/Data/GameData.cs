using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]

public class GameData
{
    public GameSceneData gameSceneData;
    public Chapter1 answerData;
    public Chapter2 answerData2;

   public GameData()
    {
        gameSceneData = new GameSceneData();
        answerData = new Chapter1();
        answerData2 = new Chapter2();
    }
}
