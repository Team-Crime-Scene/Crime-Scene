using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class TutorialData
{
    public Vector3 playerPos;
    public Quaternion playerRot;
    // 주관식 플레이어 답
    public List<string> PlayerSubAnswers1;
    // 객관식 플레이어 답
    public List<int> PlayerMultiAnswer;
    // 튜토리얼 스코어
    public int tutorialScore;

    // 피쳐 저장 데이터
    public List<GameObject> Picture;

    public List<GameObject> lines;


    public TutorialData()
    {
        PlayerSubAnswers1 = new List<string>();
        PlayerMultiAnswer = new List<int>();
        Picture = new List<GameObject>();
        lines = new List<GameObject>();
    }
}

[Serializable]
public class Chapter1Data
{
    public Vector3 playerPos;
    public Quaternion playerRot;

}