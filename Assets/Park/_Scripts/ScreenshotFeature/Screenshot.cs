using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    //ScreenshotData�� ������ Screenshot ��ü 
    public ScreenshotData Data { get; private set; }
    public ScreenshotSlotUI slotUI;

    public Screenshot( ScreenshotData data ) => Data = data;
}
