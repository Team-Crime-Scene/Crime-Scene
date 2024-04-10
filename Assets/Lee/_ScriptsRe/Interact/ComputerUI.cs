using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComputerUI : PopUpUI
{
    [SerializeField] RectTransform ComputerContent;
    Vector2 CreatePoint;
    // ����
    int score = 0;
    int backgound = 1;

    // ����� �߰�
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject answerParents;

    // �����
    [Header("Answer Sheet")]
    [SerializeField] List<string> subjecttiveAnswers1;
    [SerializeField] List<string> multipleChoiceAnswer;

    // �÷��̾� �����
    [Header("Player Answer Sheet")]
    // �ְ����� ������������� ������ ����Ʈ���� �������ְ� ����ؾߵɵ�
    [SerializeField] List<TMP_InputField> PlayerSubAnswers1 = new List<TMP_InputField>();
    // ������ �ϳ��� �ۿ� ������ ȥ�� ���� �ϸ�ɰŰ���
    [SerializeField] List<TextMeshProUGUI> PlayerMultiAnswer = new List<TextMeshProUGUI>();
    List<GameObject> addedList = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        //GetUI<TMP_InputField>("Subjecttive 1").text = "UI Binding Test";

    }
    public void CreateAnswerSheet()
    {
        addedList.Add(Instantiate(prefab, CreatePoint, Quaternion.identity, answerParents.transform));
        // ��׶��� ũ�� �����
        answerParents.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 50 * backgound);
        // ��ũ�� ũ�� �����
        ComputerContent.sizeDelta = new Vector2(0, 250 + 50 * backgound);

        PlayerSubAnswers1.Add(addedList [0].gameObject.GetComponent<TMP_InputField>());
        backgound++;
    }

    public void ClearAnswerSheet()
    {
        if ( addedList != null )
        {
            Destroy(addedList [0]);
            addedList.RemoveAt(0);
            backgound--;
            // ��׶��� ũ�� �����
            answerParents.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 50 * backgound);
            // ��ũ�� ũ�� �����
            ComputerContent.sizeDelta = new Vector2(0, 250 + 50 * backgound);
        }
    }




    public void Submit()
    {
        // �ְ��� �� üũ
        for ( int i = 0; i < subjecttiveAnswers1.Count; i++ )
        {
            for ( int j = 0; j < PlayerSubAnswers1.Count; j++ )
            {
                string answer = PlayerSubAnswers1 [j].text;
                answer = answer.Replace(" ", string.Empty);

                if ( subjecttiveAnswers1 [i] == answer )
                {
                    score++;
                }
            }
        }
        // ������ �� üũ
        for ( int i = 0; i < PlayerMultiAnswer.Count; i++ )
        {
            if ( PlayerMultiAnswer [i].text == multipleChoiceAnswer [i] )
            {
                score++;
            }
        }
        // ���� ������ ���� ��ȭ���ָ�ɵ�
        Debug.Log($"������ {score}");

    }
    private void OnDisable()
    {
/*        for ( int i = 0; i < PlayerSubAnswers1.Count; i++ )
        {
            Manager.Data.GameData.answerData.PlayerSubAnswers1 [i] = PlayerSubAnswers1 [i].text;
        }
        for ( int i = 0; i < PlayerMultiAnswer.Count; i++ )
        {
            Manager.Data.GameData.answerData.PlayerMultiAnswer [i] = PlayerMultiAnswer [i].text;
        }
        Manager.Data.SaveData();
*/    }

    public void ActivateInputField()
    {
        Manager.Game.ChangeIsChatTrue();
    }
    public void DisableInputField()
    {
        Manager.Game.ChangeIsChatFalse();
    }
}
