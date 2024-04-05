using Cinemachine;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComputerController : MonoBehaviour, IAnswerable, IZoomable
{
    // 컴퓨터에 줌되는 카메라
    [SerializeField] CinemachineVirtualCamera computerCamera;
    // 컴퓨터에 들어가는 ui
    [SerializeField] Canvas canvas;
    Vector2 CreatePoint;
    // 점수
    int score = 0;

    // 답안지 추가
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject answerParents;
    // 답안지
    [Header("Answer Sheet")]
    [SerializeField] List<string> subjecttiveAnswers;
    [SerializeField] List<string> multipleChoiceAnswer;

    // 플레이어 답안지
    [Header("Player Answer Sheet")]
    [SerializeField] List<TMP_InputField> PlayerSubAnswers = new List<TMP_InputField>();
    [SerializeField] List<TextMeshProUGUI> PlayerMultiAnswer = new List<TextMeshProUGUI>();

    [SerializeField] List<GameObject> addedList = new List<GameObject>();
    public void CreateAnswerSheet()
    {
        addedList.Add(Instantiate(prefab, CreatePoint, Quaternion.identity, answerParents.transform));
        PlayerSubAnswers.Add(addedList [0].gameObject.GetComponent<TMP_InputField>());
    }

    public void ClearAnswerSheet()
    {
        if ( addedList != null )
        {
            Destroy(addedList [0]);
            addedList.RemoveAt(0);
        }
    }
    public void SubmitButton()
    {
        // 주관식 답 체크
        for ( int i = 0; i < PlayerSubAnswers.Count; i++ )
        {
            string answer = PlayerSubAnswers [i].text;
            Debug.Log(answer);
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

    public void UnzoomObject( Transform ZoomTrans )
    {
        computerCamera.m_Priority = 0;
        canvas.enabled = false;
    }

    public void ZoomObject( Transform ZoomTrans )
    {
        computerCamera.m_Priority = 20;
        canvas.enabled = true;
    }


}