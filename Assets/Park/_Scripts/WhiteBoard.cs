using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WhiteBoard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public LineRenderer lineRenderer;

    private List<List<Vector3>> allLines = new List<List<Vector3>>(); // 모든 선 
    private List<Color> colors = new List<Color>(); //선 색깔 정보
    private List<Vector3> currentLine = new List<Vector3>(); //현재 그리는 선 

    private bool isDrawing = false;
    private int positionCount = 2;

    public Color brushColor = Color.black;
    public float brushSize = 5f;


    void Start()
    {
        lineRenderer.startWidth = brushSize;
        lineRenderer.endWidth = brushSize;

        lineRenderer.positionCount = positionCount;
        lineRenderer.startColor = brushColor;
        lineRenderer.endColor = brushColor;
    }



    /******************************************************
     *          Pointer Down/Up & Drag Interface
     ******************************************************/
    #region Interfaces

    public void OnPointerDown( PointerEventData eventData )
    {
        isDrawing = true;
        currentLine.Clear();
        AddPoint(eventData.pointerCurrentRaycast.worldPosition);
        AddColor(lineRenderer.startColor);
        RenderAllLines();
    }

    public void OnPointerUp( PointerEventData eventData )
    {
        isDrawing = false;
        if ( currentLine.Count > 0 )
        {
            allLines.Add(new List<Vector3>(currentLine));
            currentLine.Clear();
        }
        RenderAllLines();
    }

    public void OnDrag( PointerEventData eventData )
    {

        if ( isDrawing )
        {
            AddPoint(GetLocalPosition(eventData));
            RenderAllLines();
        }
    }
    #endregion


    /******************************************************
     *                  Draw Methods & ETC
     ******************************************************/
    #region Draw Methods & ETC
    private Vector3 GetLocalPosition( PointerEventData eventData )
    {
        Vector3 worldPosition = eventData.pointerCurrentRaycast.worldPosition;
        return transform.InverseTransformPoint(worldPosition);
    }
    private void AddPoint( Vector3 position )
    {
        currentLine.Add(position);
    }

    private void AddColor( Color color )
    {
        colors.Add(color); 
    }

    private void SetColor(Color color)
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }

    private void RenderCurrentLine() //RenderAllLines에 병합됨
    {
        lineRenderer.positionCount = currentLine.Count;
        lineRenderer.SetPositions(currentLine.ToArray());
    }

    private void RenderAllLines()
    {
        int totalPoints = currentLine.Count;
        foreach ( List<Vector3> line in allLines )
        {
            totalPoints += line.Count;
        }

        Vector3 [] allPositions = new Vector3 [totalPoints];
        int index = 0;
        currentLine.CopyTo(allPositions, index);
        index += currentLine.Count;
        foreach ( List<Vector3> line in allLines )
        {
            line.CopyTo(allPositions, index);
            index += line.Count;
        }

        lineRenderer.positionCount = totalPoints;
        lineRenderer.SetPositions(allPositions);
    }
    #endregion


    /******************************************************
     *                  UI OnClick Events
     ******************************************************/
    #region OnClick
    public void SetColorButton(int color) //매개변수가 Enum이라 OnClick에 등록 불가
    {
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
        lineRenderer.startColor = newColor;
        lineRenderer.endColor = newColor;
    }

    public void EraseAll()
    {
        allLines.Clear();
        RenderAllLines();
    }

    public void Undo()
    {
        // 현재 레이어의 선을 다 지움
        // 레이어 --
    }
    #endregion
}
