using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotSlotUI : MonoBehaviour
{
    //Album UI안의 각 스크린샷 슬롯

    // 슬롯의 인덱스 관리, 슬롯의 Screenshot 이미지, 하이라이트, 클릭시 SelectedScreenshot UI의 이미지를 교체
    [SerializeField] Image image;
    [SerializeField] public string path;


    private void Start()
    {
        image.sprite = Extension.LoadSprite(path);
    }
}
