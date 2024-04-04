using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]

public class GameData
{
    public GameSceneData gameSceneData;

   public GameData()
    {
        gameSceneData = new GameSceneData();
    }
}
