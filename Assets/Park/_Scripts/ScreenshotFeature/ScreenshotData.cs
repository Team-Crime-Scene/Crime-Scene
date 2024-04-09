using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotData :ScriptableObject
{
    //Screenshot�� Data�� �ٷ�� ��ũ��Ʈ

    public ScreenshotData(string path )
    {
        this.path = path;
    }

    public string path;
    public bool isBookmarked;
    //public string imageTag;  // Screenshot�� ���� tag�� ������ �Ǻ��ϰ��� �� �� ��� 
    public GameObject prefab; // ȭ��Ʈ ���忡 ���� Prefab, �������ְ� �ƴҼ����ְ�...
}
