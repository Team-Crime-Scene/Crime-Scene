using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComputerChapter1UI : PopUpUI
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
    [SerializeField] string subjecttiveAnswer1;
    [SerializeField] List<string> subjecttiveAnswers2;
    [SerializeField] string subjecttiveAnswer3;
    [SerializeField] List<string> multipleChoiceAnswer;

    // 플레이어 답안지
    [Header("Player Answer Sheet")]
    [SerializeField] TMP_InputField PlayerSubAnswers1;
    // 주관식은 만들어질때마다 보관할 리스트들을 생성해주고 사용해야될듯
    // 얘는 걍 이대로 쓰면됨
    [SerializeField] List<TMP_InputField> PlayerSubAnswers2 = new List<TMP_InputField>();
    [SerializeField] TMP_InputField PlayerSubAnswers3;

    // 문제당 하나씩 밖에 없으니 혼자 관리 하면될거같다
    [SerializeField] List<TextMeshProUGUI> PlayerMultiAnswer = new List<TextMeshProUGUI>();
    List<GameObject> addedList = new List<GameObject>();

    protected override void Awake()
    {
        Manager.Data.LoadData();
        for ( int i = 0; i < Manager.Data.GameData.tutorialData.PlayerSubAnswers1.Count; i++ )
        {
            if ( i > 0 )
            {
                // 답안지 추가
                GameObject addList = Instantiate(prefab, CreatePoint, Quaternion.identity, answerParents.transform);
                addedList.Add(addList);
                PlayerSubAnswers2.Add(addList.GetComponent<TMP_InputField>());
                // 백그라운드 크게 만들기
                answerParents.GetComponent<RectTransform>().sizeDelta = new Vector2(answerParents.GetComponent<RectTransform>().sizeDelta.x, 70 * backgound);
                // 스크롤 크게 만들기
                ComputerContent.sizeDelta = new Vector2(ComputerContent.sizeDelta.x, 250 + 130 * backgound);
                backgound++;
            }
        }
        base.Awake();
        //GetUI<TMP_InputField>("Subjecttive 1").text = "UI Binding Test";
    }
    private void Start()
    {
        Manager.Data.LoadAnswer(PlayerSubAnswers2, PlayerMultiAnswer);
    }
    public void CreateAnswerSheet()
    {
        // 답안지 추가
        addedList.Add(Instantiate(prefab, CreatePoint, Quaternion.identity, answerParents.transform));
        // 백그라운드 크게 만들기
        answerParents.GetComponent<RectTransform>().sizeDelta = new Vector2(answerParents.GetComponent<RectTransform>().sizeDelta.x, 70 * backgound);
        // 스크롤 크게 만들기
        ComputerContent.sizeDelta = new Vector2(ComputerContent.sizeDelta.x, 250 + 130 * backgound);

        PlayerSubAnswers2.Add(addedList [0].gameObject.GetComponent<TMP_InputField>());
        backgound++;
    }

    public void ClearAnswerSheet()
    {
        if ( addedList != null )
        {
            Destroy(addedList [0]);
            addedList.RemoveAt(0);
            PlayerSubAnswers2.RemoveAt(1);
            backgound--;
            // 백그라운드 크게 만들기
            answerParents.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 70 * backgound);
            // 스크롤 크게 만들기
            ComputerContent.sizeDelta = new Vector2(0, 250 + 130 * backgound);
        }
    }

    public void Submit()
    {
        string answer;
        answer = PlayerSubAnswers1.text;
        answer = answer.Replace(" ", string.Empty);
        if ( subjecttiveAnswer1 == answer )
            score++;
        // 주관식 답 체크(추가되는 친구)
        for ( int i = 0; i < subjecttiveAnswers2.Count; i++ )
        {
            for ( int j = 0; j < PlayerSubAnswers2.Count; j++ )
            {
                answer = PlayerSubAnswers2 [j].text;
                answer = answer.Replace(" ", string.Empty);

                if ( subjecttiveAnswers2 [i] == answer )
                {
                    score++;
                }
            }
        }
        answer = PlayerSubAnswers3.text;
        answer = answer.Replace(" ", string.Empty);
        if ( subjecttiveAnswer3 == answer )
            score++;

        // 객관식 답 체크
        for ( int i = 0; i < PlayerMultiAnswer.Count; i++ )
        {
            if ( PlayerMultiAnswer [i].text == multipleChoiceAnswer [i] )
            {
                score++;
            }
        }
        // 여기 끝날때 씬을 변화해주면될듯 (저장, 씬 이동)
        //Manager.Data.SaveAnswer(PlayerSubAnswers2, PlayerMultiAnswer, score);
        Debug.Log($"점수는 {score}");

    }
    private void OnDisable()
    {
        //Manager.Data.SaveAnswer(PlayerSubAnswers2, PlayerMultiAnswer, score);
    }

    public void ActivateInputField()
    {
        Manager.Game.ChangeIsChatTrue();
    }
    public void DisableInputField()
    {
        Manager.Game.ChangeIsChatFalse();
    }
}
