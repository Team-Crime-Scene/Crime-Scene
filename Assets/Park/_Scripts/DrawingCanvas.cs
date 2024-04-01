using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class DrawingCanvas : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public LineRenderer lineRenderer;
    private Vector3 [] linePositions = new Vector3 [0];

    private List<List<Vector3>> allLines = new List<List<Vector3>>(); // 모든 선 데이터
    private List<Vector3> currentLine = new List<Vector3>();

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

    public void OnPointerDown( PointerEventData eventData )
    {
        isDrawing = true;
        currentLine.Clear();
        AddPoint(eventData.pointerCurrentRaycast.worldPosition);
    }

    public void OnPointerUp( PointerEventData eventData )
    {
        isDrawing = false;
        if ( currentLine.Count > 0 )
        {
            allLines.Add(new List<Vector3>(currentLine));
            currentLine.Clear();
            RenderAllLines();
        }
    }

    public void OnDrag( PointerEventData eventData )
    {
       
        if ( isDrawing )
        {
            AddPoint(GetLocalPosition(eventData));
            RenderCurrentLine();
            RenderAllLines();
        }
    }

    private void AddPoint( Vector3 position )
    {
        currentLine.Add(position);
    }

    private Vector3 GetLocalPosition( PointerEventData eventData )
    {
        Vector3 worldPosition = eventData.pointerCurrentRaycast.worldPosition;
        return transform.InverseTransformPoint(worldPosition);
    }

    private void RenderCurrentLine()
    {
        lineRenderer.positionCount = currentLine.Count;
        lineRenderer.SetPositions(currentLine.ToArray());
    }

    private void RenderAllLines()
    {
        int totalPoints = 0;
        foreach ( List<Vector3> line in allLines )
        {
            totalPoints += line.Count;
        }

        Vector3 [] allPositions = new Vector3 [totalPoints];
        int index = 0;
        foreach ( List<Vector3> line in allLines )
        {
            line.CopyTo(allPositions, index);
            index += line.Count;
        }

        lineRenderer.positionCount = totalPoints;
        lineRenderer.SetPositions(allPositions);
    }
}