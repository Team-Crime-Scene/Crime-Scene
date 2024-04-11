using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniSlotUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image image;
    Screenshot screenshot;
    public Screenshot Screenshot {  get { return screenshot; } set { screenshot = value; } }

    [SerializeField] Picture prefab;

    Vector3 offset = new Vector3(0,0,-0.1f);
    private void Start()
    {
        image.sprite = Extension.LoadSprite(screenshot.Data.path);
    }

    public void OnPointerClick( PointerEventData eventData )
    {
        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        RaycastHit hit;

        // 월드 공간에 Raycast
        if ( Physics.Raycast(ray, out hit) )
        {
            Picture picture = Instantiate(prefab, hit.point+offset, Quaternion.identity);
            picture.SetSprite(image);
        }
    }

    public void Delete()
    {
        Debug.Log($"이거 지웠음 => {screenshot.Data.path}");
        Destroy(gameObject);
    }
}
