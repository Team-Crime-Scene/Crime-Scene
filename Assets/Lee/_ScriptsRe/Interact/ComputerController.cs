using Cinemachine;
using System.Collections;
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
    // 제출 버튼
    [SerializeField] Button submit;

    // 답안지
    [Header("Answer Sheet")]
    [SerializeField] List<string> subjecttiveAnswers;
    [SerializeField] List<string> multipleChoiceAnswer;

    // 플레이어 답안지
    [Header("Player Answer Sheet")]
    [SerializeField] List<TMP_InputField> PlayerSubAnswers = new List<TMP_InputField>() ;
    [SerializeField] List<TextMeshProUGUI> PlayerMultiAnswer = new List<TextMeshProUGUI>() ;

    public void SubmitButton()
    {
        if( PlayerSubAnswers [0].text == subjecttiveAnswers [0] )
        {

        }
        
    }

    public void OpenAnwer()
    {

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