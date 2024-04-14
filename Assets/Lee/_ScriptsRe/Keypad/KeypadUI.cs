using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeypadUI : MonoBehaviour, IInteractable
{
    [SerializeField] string answerNumber;
    string input; // 현재 입력된 값
    [SerializeField] TextMeshProUGUI displayText; // 키패드에 입력된 비밀번호가 표시되는 텍스트 영역
    float numOfGuesses; // 예상 비밀번호의 길이
    float btnClicked = 0; // 버튼이 클릭된 횟수
    [SerializeField] CinemachineVirtualCamera Vcam;
    [SerializeField] PopUpUI popUpUI;

    public void Interact( PlayerController player )
    {
        Vcam.Priority = 100;
        Manager.UI.ShowPopUpUI( popUpUI);
    }

    public void UnInteract( PlayerController player )
    {
        Vcam.Priority = 0;

    }

    private void Start()
    {

        btnClicked = 0; // 버튼이 클릭된 횟수 초기화
        numOfGuesses = answerNumber.Length; // 비밀번호 길이
    }
    public void CheckButton()
    {
        if ( btnClicked == numOfGuesses ) // 버튼이 클릭된 횟수가 예상 비밀번호의 길이와 같은 경우
        {
            if ( input == answerNumber ) // 입력된 값이 예상 비밀번호와 같은지 확인
            {
                Debug.Log("정답"); // 비밀번호가 올바르다는 메시지를 출력
                input = ""; // 입력 초기화
                btnClicked = 0; // 버튼 클릭 횟수 초기화
            }
            else
            {
                //Reset input varible
                input = ""; // 입력 초기화
                displayText.text = input.ToString(); // 입력된 값을 텍스트 영역에 표시
                Debug.Log("틀림");

            }
        }
    }

    public void ValueEntered( string valueEntered ) // 키패드에 값 입력 시 호출되는 함수
    {
        switch ( valueEntered ) // 입력된 값에 따라 분기
        {
            case "Q": // QUIT
                btnClicked = 0; // 버튼 클릭 횟수 초기화
                input = ""; // 입력 초기화
                displayText.text = input.ToString(); // 텍스트 영역 초기화
                break;

            case "C": // CLEAR
                input = ""; // 입력 초기화
                btnClicked = 0; // 버튼 클릭 횟수 초기화
                displayText.text = input.ToString(); // 텍스트 영역 초기화
                break;

            default: // Buton clicked add a variable
                btnClicked++; // 버튼 클릭 횟수 증가
                input += valueEntered; // 입력된 값 추가
                displayText.text = input.ToString(); // 텍스트 영역에 입력된 값 표시
                break;
        }
        CheckButton();
    }

}
