using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotData :ScriptableObject
{
    //Screenshot의 Data를 다루는 스크립트

    public ScreenshotData(string path )
    {
        this.path = path;
    }

    public string path;
    public bool isBookmarked;
    //public string imageTag;  // Screenshot이 가진 tag로 정답을 판별하고자 할 때 사용 
}
