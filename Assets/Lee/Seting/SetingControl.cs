using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetingControl : MonoBehaviour
{
    bool timeStop = false;
    // esc�� ����ϴ� ��� UI�� ���⼭ Ȱ��ȭ������ ����
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
