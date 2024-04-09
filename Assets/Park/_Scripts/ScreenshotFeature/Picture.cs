using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Picture : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler  ,IPointerEnterHandler , IPointerExitHandler
{
    [SerializeField] Image screenshot;
    [SerializeField] Image highLight;
    [SerializeField] TextMeshProUGUI text;

    float dragSpeed = 0.65f;
    private Transform prevTransform; 

    public void SetSprite(Image image)
    {
        screenshot.sprite = image.sprite;
    }


    /******************************************************
     *             Mouse Pointer  Interfaces
     ******************************************************/

    public void OnDrag( PointerEventData eventData )
    {
        Vector3 delta = eventData.delta;
        delta.z = 0;
        transform.position += delta*Time.deltaTime*dragSpeed;
    }

    public void OnPointerDown( PointerEventData eventData )
    {
        //if 만약 내려 놓은 곳이 화이트보드 영역 밖이라면
        // transform.position = prevTransform.position;  이전 위치로 다시 돌아감 / 혹은 Destroy(gameObject) 
    }

    public void OnPointerUp( PointerEventData eventData )
    {
    }



    public void OnPointerEnter( PointerEventData eventData )
    {
        highLight.enabled = true;
    }

    public void OnPointerExit( PointerEventData eventData )
    {
        highLight.enabled = false;
    }
}
