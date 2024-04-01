using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetingControl : MonoBehaviour
{
    bool timeStop = false;
    // esc를 사용하는 모든 UI는 여기서 활성화시켜줄 예정
    public void OnStop( InputValue value )
    {
        if ( timeStop == false )
        {
            
            Time.timeScale = 0;
            timeStop = true;
        }
        else
        {
            Time.timeScale = 1;
            timeStop = false;
        }

    }
}
