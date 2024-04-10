using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeypadUI : PopUpUI
{
    [SerializeField] string answerNumber;
    string input; // ���� �Էµ� ��
    [SerializeField] TextMeshProUGUI displayText; // Ű�е忡 �Էµ� ��й�ȣ�� ǥ�õǴ� �ؽ�Ʈ ����
    float numOfGuesses; // ���� ��й�ȣ�� ����
    float btnClicked = 0; // ��ư�� Ŭ���� Ƚ��
    private void Start()
    {
        btnClicked = 0; // ��ư�� Ŭ���� Ƚ�� �ʱ�ȭ
        numOfGuesses = answerNumber.Length; // ��й�ȣ ����
    }
    void Update()
    {
        if ( btnClicked == numOfGuesses ) // ��ư�� Ŭ���� Ƚ���� ���� ��й�ȣ�� ���̿� ���� ���
        {
            if ( input == answerNumber ) // �Էµ� ���� ���� ��й�ȣ�� ������ Ȯ��
            {
                Debug.Log("����"); // ��й�ȣ�� �ùٸ��ٴ� �޽����� ���
                input = ""; // �Է� �ʱ�ȭ
                btnClicked = 0; // ��ư Ŭ�� Ƚ�� �ʱ�ȭ
            }
            else
            {
                //Reset input varible
                input = ""; // �Է� �ʱ�ȭ
                displayText.text = input.ToString(); // �Էµ� ���� �ؽ�Ʈ ������ ǥ��
                Debug.Log("Ʋ��");
            }
        }
    }

    public void ValueEntered( string valueEntered ) // Ű�е忡 �� �Է� �� ȣ��Ǵ� �Լ�
    {
        switch ( valueEntered ) // �Էµ� ���� ���� �б�
        {
            case "Q": // QUIT
                btnClicked = 0; // ��ư Ŭ�� Ƚ�� �ʱ�ȭ
                input = ""; // �Է� �ʱ�ȭ
                displayText.text = input.ToString(); // �ؽ�Ʈ ���� �ʱ�ȭ
                break;

            case "C": // CLEAR
                input = ""; // �Է� �ʱ�ȭ
                btnClicked = 0; // ��ư Ŭ�� Ƚ�� �ʱ�ȭ
                displayText.text = input.ToString(); // �ؽ�Ʈ ���� �ʱ�ȭ
                break;

            default: // Buton clicked add a variable
                btnClicked++; // ��ư Ŭ�� Ƚ�� ����
                input += valueEntered; // �Էµ� �� �߰�
                displayText.text = input.ToString(); // �ؽ�Ʈ ������ �Էµ� �� ǥ��
                break;
        }
    }

}
