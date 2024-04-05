using Cinemachine;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComputerController : MonoBehaviour,IInteractable
{
    // 컴퓨터에 줌되는 카메라
    [SerializeField] CinemachineVirtualCamera vCam;
    // 컴퓨터에 들어가는 ui
    [SerializeField] GameObject computerPanel;
    // 점수
    int score = 0;
    int totalQuestions = 0;


    // 답안지
    [Header("Answer Sheet")]
    [SerializeField] List<string> subjecttiveAnswers;
    [SerializeField] List<string> multipleChoiceAnswer;

    // 플레이어 답안지
    [Header("Player Answer Sheet")]
    [SerializeField] List<TMP_InputField> PlayerSubAnswers = new List<TMP_InputField>();
    [SerializeField] List<TextMeshProUGUI> PlayerMultiAnswer = new List<TextMeshProUGUI>();

    public void Interact( PlayerController player )
    {
        vCam.Priority = 20;
        computerPanel.SetActive(true);
    }

    public void Submit()
    {
        totalQuestions += subjecttiveAnswers.Count;
        totalQuestions += multipleChoiceAnswer.Count;
        // 주관식 답 체크
        for ( int i = 0; i < PlayerSubAnswers.Count; i++ )
        {
            string answer = PlayerSubAnswers [i].text;
            answer = answer.Replace(" ", string.Empty);
            if ( answer == subjecttiveAnswers [i] )
            {
                score++;
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

    public void UnInteract( PlayerController player )
    {
        vCam.Priority = 0;
        computerPanel.SetActive(false);
    }

}