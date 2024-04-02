using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScreenshotTest : MonoBehaviour
{
    // 테스트용 스크립트
    // C로 사진찍는 행동 자체는 일시정지 이외면 다 되야해서 GameManager가 가지고 있어야 할 듯
    [SerializeField] ScreenshotSystem screenshotSystem;

    void OnCapture(InputValue inputValue )
    {
        if(inputValue.isPressed)
        {
            screenshotSystem.isTakeScreenshot = true;
        }
    }
}
