using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComputerUI : PopUpUI
{
    
    // 답안지
    [Header("Answer Sheet")]
    [SerializeField] List<string> subjecttiveAnswers;
    [SerializeField] List<string> multipleChoiceAnswer;

    // 플레이어 답안지
    [Header("Player Answer Sheet")]
    [SerializeField] List<TMP_InputField> PlayerSubAnswers = new List<TMP_InputField>();
    [SerializeField] List<TextMeshProUGUI> PlayerMultiAnswer = new List<TextMeshProUGUI>();

    protected override void Awake()
    {
        base.Awake();

        GetUI<TMP_InputField>("Subjecttive 1").text = "UI Binding Test";
    }


    private void OnDisable()
    {

        //저장데이터에 = GetUI<TMP_InputField>("Subjecttive 1").text  저장하면됨
    }

}
