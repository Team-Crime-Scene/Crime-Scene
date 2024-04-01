using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnhancedWhiteBoard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IDragHandler
{
    [SerializeField] LineRenderer linePrefab;
    [SerializeField] Color color;

    private List<LineRenderer> lines = new List<LineRenderer>();
    private LineRenderer curLine;

    private bool isDrawing;

    /******************************************************
     *          Pointer Down/Up & Drag Interface
     ******************************************************/
    #region Interfaces

    public void OnPointerDown( PointerEventData eventData )
    {
        isDrawing = true;

        Vector3 downPos = GetLocalPosition(eventData);

        curLine = Instantiate(linePrefab, transform);
        curLine.startColor = color;
        curLine.endColor = color;
        lines.Add(curLine);

        Vector3 [] positions = new Vector3 [1];
        positions [0] = downPos;
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

    public void OnDrag( PointerEventData eventData )
    {
        if ( isDrawing == false )
            return;

        Vector3 [] positions = new Vector3 [curLine.positionCount + 1];
        curLine.GetPositions(positions);
        Vector3 downPos = GetLocalPosition(eventData);

        positions [curLine.positionCount] = downPos;

        curLine.positionCount++;
        curLine.SetPositions(positions);
    }
    #endregion


    /******************************************************
     *                  Draw Methods & ETC
     ******************************************************/

    private Vector3 GetLocalPosition( PointerEventData eventData )
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
        //매개변수가 Enum이라 OnClick에 등록 불가
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

    public void EraseAll()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            Destroy(lines [i].gameObject);
        }
        lines.Clear();
    }

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