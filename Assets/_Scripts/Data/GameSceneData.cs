using System;
using UnityEngine;

[Serializable]
// �̰� ���� �����ϰ� �ִ� �� ������
public class GameSceneData
{
    public Vector3 playerPos;
    public Quaternion playerRot;
    public string PlayerSubAnswers;
    public string PlayerMultiAnswer;


    public GameSceneData()
    {
        // �ʱ�ȭ���� ��
    }
}
