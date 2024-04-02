using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnhancedWhiteBoard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IDragHandler
{
    //선을 그릴 때 마다 새로운 LineRender 객체를 생성하는 버전

    // 장점 : 선을 개별로 관리하기 용이함. 나중에 사진 객체를 보드에 추가할때 SortingLayer 설정을 따로 해줄 수 있음
    // 단점 : Save&Load 시 부하가 큼

    [SerializeField] LineRenderer linePrefab;
    [SerializeField] Color color; 

    private List<LineRenderer> lines = new List<LineRenderer>();
    private LineRenderer curLine;

    private bool isDrawing;

    /******************************************************
     *             Mouse Pointer  Interfaces
     ******************************************************/
    #region Interfaces

    public void OnPointerDown( PointerEventData eventData ) 
    {
        isDrawing = true;

        Vector3 downPos = eventData.GetLocalPosition(transform);

        curLine = Instantiate(linePrefab, transform);
        curLine.startColor = color;
        curLine.endColor = color;
        lines.Add(curLine);

        Vector3 [] positions = new Vector3 [1];
        positions [0] = downPos;
        curLine.SetPositions(positions);
    }
    public void OnDrag( PointerEventData eventData )
    {
        if ( isDrawing == false )
            return;

        Vector3 [] positions = new Vector3 [curLine.positionCount + 1];
        curLine.GetPositions(positions);
        Vector3 downPos = eventData.GetLocalPosition(transform);

        positions [curLine.positionCount] = downPos;

        curLine.positionCount++;
        curLine.SetPositions(positions);
    }

    public void OnPointerUp( PointerEventData eventData )
    {
        isDrawing = false;
    }

    public void OnPointerExit( PointerEventData eventData )
    {
        isDrawing = false;
    }

    #endregion


    /******************************************************
     *                         ETC
     ******************************************************/

    private Vector3 GetLocalPosition( PointerEventData eventData ) //Extension에 확장메서드로 작성해둠. 나중에 지울 것
    {
        Vector3 worldPosition = eventData.pointerCurrentRaycast.worldPosition;
        return transform.InverseTransformPoint(worldPosition);
    }

    public void SetColor( Color color )
    {
        this.color = color;
    }

    /******************************************************
     *                  UI OnClick Events
     ******************************************************/

    public void SetColorButton( int color )
    {
        //매개변수가 Enum이면 OnClick에 등록 불가...
        Color newColor = new Color();
        // 0 = black, 1 = red, 2 = blue
        switch ( color )
        {
            case 0:
                newColor = Color.black;
                break;
            case 1:
                newColor = Color.red;
                break;
            case 2:
                newColor = Color.blue;
                break;
        }
        SetColor(newColor);
    }

    //모든 선을 지움
    public void EraseAll()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            Destroy(lines [i].gameObject);
        }
        lines.Clear();
    }

    //이전에 그었던 선을 지움
    public void Undo()
    {
        if ( lines.Count <= 0 )
            return;

        Destroy(lines [lines.Count - 1].gameObject); 
        lines.RemoveAt(lines.Count - 1);
    }
}

struct PointData
{
    public Vector3 position;
    public Color color;
}