using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    //ScreenshotData¸¦ °¡Áö´Â Screenshot °´Ã¼ 
    public ScreenshotData Data { get; private set; }

    public Screenshot( ScreenshotData data ) => Data = data;
}
