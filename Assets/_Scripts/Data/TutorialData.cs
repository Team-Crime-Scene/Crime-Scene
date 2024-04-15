using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    /*    public List<PictureData> pictureData;*/
    // 라인 저장 데이터
    public List<LineData> lineDatas;

    public TutorialData()
    {
        PlayerSubAnswers1 = new List<string>();
        PlayerMultiAnswer = new List<int>();
        lineDatas = new List<LineData>();
    }
}

[Serializable]
public class Chapter1Data
{
    public Vector3 playerPos;
    public Quaternion playerRot;

}
/*[Serializable]
public struct PictureData
{
    public Transform transform;
    public Image image;
}
*/
[Serializable]
public struct LineData
{
    public Color color;
    public int count;
    public Vector3 [] pos;
}