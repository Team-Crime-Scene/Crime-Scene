using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Picture : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image screenshot;
    [SerializeField] Image outLine;
    [SerializeField] TextMeshProUGUI text;

    [SerializeField] Color selectedColor;
    [SerializeField] Color deleteColor;
    private Color defaultColor;

    [SerializeField][Range(0, 1000)] float dragSpeed;
    float height;
    float width;

    public void SetSprite(Image image)
    {
        screenshot.sprite = image.sprite;
    }
    public void Start()
    {
        width = Screen.width;
        height = Screen.height;
    }


    /******************************************************
     *             Mouse Pointer  Interfaces
     ******************************************************/

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 delta = eventData.delta;
         delta = new Vector3(
            delta.x / width, 
            delta.y / height, 
            0f);
        //Vector3.Scale(eventData.delta, new Vector3(1.0f / width, 1.0f / height, 1.0f))고려

        transform.position += delta * Time.deltaTime * dragSpeed;


        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            if (hit.collider.GetComponent<EnhancedWhiteBoard>() != null)
            {
                outLine.color = selectedColor;
            }
            else
            {
                outLine.color = deleteColor;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //만약 내려 놓은 곳이 화이트보드 영역 밖이라면 삭제 
        if (outLine.color == deleteColor)
        {
            Destroy(gameObject);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        outLine.color = selectedColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outLine.color = defaultColor;
    }
}
