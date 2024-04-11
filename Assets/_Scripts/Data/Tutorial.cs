using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Tutorial
{
    public Vector3 playerPos;
    public Quaternion playerRot;
    // 주관식 플레이어 답
    public List<string> PlayerSubAnswers1 = new List<string>();
    // 객관식 플레이어 답
    public List<string> PlayerMultiAnswer = new List<string>();
    // 튜토리얼 스코어
    public int tutorialScore;

    public Tutorial()
    {
    }
}

[Serializable]
public class Chapter1
{
    public Vector3 playerPos;
    public Quaternion playerRot;

}