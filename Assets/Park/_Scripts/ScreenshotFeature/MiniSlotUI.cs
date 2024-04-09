using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniSlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;
    [SerializeField] public Screenshot screenshot;

    [SerializeField] Picture prefab;

    private void Start()
    {
        image.sprite = Extension.LoadSprite(screenshot.Data.path);
    }

    public void OnPointerClick( PointerEventData eventData )
    {
        //클릭시 화이트 보드에 붙일 수 있는 prefab 생성
        Picture picture = Instantiate(prefab, eventData.position, transform.rotation); //생성 좌표 다시 계산해줘야함
        picture.SetSprite(image);
    }
}
