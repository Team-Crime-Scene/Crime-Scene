using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComputerUI : PopUpUI
{
    [SerializeField] RectTransform ComputerContent;
    Vector2 CreatePoint;
    // 점수
    int score = 0;
    int backgound = 1;

    // 답안지 추가
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject answerParents;

    // 답안지
    [Header("Answer Sheet")]
    [SerializeField] List<string> subjecttiveAnswers1;
    [SerializeField] List<string> multipleChoiceAnswer;

    // 플레이어 답안지
    [Header("Player Answer Sheet")]
    // 주관식은 만들어질때마다 보관할 리스트들을 생성해주고 사용해야될듯
    [SerializeField] List<TMP_InputField> PlayerSubAnswers1 = new List<TMP_InputField>();
    // 문제당 하나씩 밖에 없으니 혼자 관리 하면될거같다
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
        // 백그라운드 크게 만들기
        answerParents.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 50 * backgound);
        // 스크롤 크게 만들기
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
            // 백그라운드 크게 만들기
            answerParents.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 50 * backgound);
            // 스크롤 크게 만들기
            ComputerContent.sizeDelta = new Vector2(0, 250 + 50 * backgound);
        }
    }




    public void Submit()
    {
        // 주관식 답 체크
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
        // 객관식 답 체크
        for ( int i = 0; i < PlayerMultiAnswer.Count; i++ )
        {
            if ( PlayerMultiAnswer [i].text == multipleChoiceAnswer [i] )
            {
                score++;
            }
        }
        // 여기 끝날때 씬을 변화해주면될듯
        Debug.Log($"점수는 {score}");

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
