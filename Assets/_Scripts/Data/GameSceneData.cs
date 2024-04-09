using System;
using UnityEngine;

[Serializable]
// 이게 지금 실험하고 있는 씬 데이터
public class GameSceneData
{
    public Vector3 playerPos;
    public Quaternion playerRot;
    public string PlayerSubAnswers;
    public string PlayerMultiAnswer;


    public GameSceneData()
    {
        // 초기화해줄 곳
    }
}
