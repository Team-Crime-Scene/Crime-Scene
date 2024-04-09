using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WhiteBoard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler ,IPointerExitHandler
{
    // ������ ���� �ش�Ǵ� LineRenderer�� ������ �̸� �̿��� ���� �׸��� ����

    // ���� : Save & Load�� ���ϰ� ����
    // ���� : ���� ���� ���� ��°�� ���� ��ü�̱� ������ ���߿� ���� ����� �߰��� �� ������ ���� SortingLayer ������ ���� �� ����
    [SerializeField] LineRenderer lineRendererBlack;
    [SerializeField] LineRenderer lineRendererRed;
    [SerializeField] LineRenderer lineRendererBlue;

    [SerializeField] Color currentColor = Color.black;
    [SerializeField] float brushSize = 0.03f; // �� ����

    private List<List<PointData>> allLines = new List<List<PointData>>(); 
    private List<PointData> currentLine = new List<PointData>(); 

    private bool isDrawing = false;

    void Start()
    {
        InitLineRenderer();
    }
    /******************************************************
     *              Mouse Pointer Interfaces
     ******************************************************/
    #region Interfaces

    public void OnPointerDown( PointerEventData eventData )
    {
        isDrawing = true;
        currentLine.Clear();

        Vector3 downPos = eventData.GetLocalPosition(transform);
        AddPoint(new Vector3(downPos.x, downPos.y, 0)); 
        RenderAllLines();
    }
    public void OnDrag( PointerEventData eventData )
    {

        if ( isDrawing )
        {
            AddPoint(eventData.GetLocalPosition(transform)); 
            RenderAllLines();
        }
    }

    public void OnPointerUp( PointerEventData eventData )
    {
        isDrawing = false;
        if ( currentLine.Count > 0 )
        {
            allLines.Add(new List<PointData>(currentLine));
            currentLine.Clear();
        }
        RenderAllLines();
    }

    public void OnPointerExit( PointerEventData eventData )
    {
        isDrawing=false;
    }

    #endregion


    /******************************************************
     *                  Draw Methods & ETC
     ******************************************************/
    #region Draw Methods & ETC

    private void InitLineRenderer()
    {
        lineRendererBlack.startWidth = brushSize;
        lineRendererBlack.endWidth = brushSize;

        lineRendererRed.startWidth = brushSize;
        lineRendererRed.endWidth = brushSize;

        lineRendererBlue.startWidth = brushSize;
        lineRendererBlue.endWidth = brushSize;
    }

    private void AddPoint( Vector3 position )
    {
        PointData pointData = new PointData
        {
            position = position,
            color = currentColor
        };
        currentLine.Add(pointData);
    }

    public void SetColor( Color color )
    {
        currentColor = color;
    }

    private void RenderAllLines()
    {
        int totalPoints = currentLine.Count;
        foreach ( List<PointData> line in allLines )
        {
            totalPoints += line.Count;
        }

        Vector3 [] redPositions = new Vector3 [totalPoints];
        Vector3 [] bluePositions = new Vector3 [totalPoints];
        Vector3 [] blackPositions = new Vector3 [totalPoints];

        int index = 0;
        foreach ( PointData pointData in currentLine )
        {
            AddPointToLineRenderer(pointData, redPositions, bluePositions, blackPositions, ref index);
        }
        foreach ( List<PointData> line in allLines )
        {
            foreach ( PointData pointData in line )
            {
                AddPointToLineRenderer(pointData, redPositions, bluePositions, blackPositions, ref index);
            }
        }

        lineRendererRed.positionCount = index;
        lineRendererRed.SetPositions(redPositions);
        lineRendererBlue.positionCount = index;
        lineRendererBlue.SetPositions(bluePositions);
        lineRendererBlack.positionCount = index;
        lineRendererBlack.SetPositions(blackPositions);
    }

    private void AddPointToLineRenderer( PointData pointData, Vector3 [] redPositions, Vector3 [] bluePositions, Vector3 [] blackPositions, ref int index )
    {
        if ( pointData.color == Color.red )
        {
            redPositions [index] = pointData.position;
        }
        else if ( pointData.color == Color.blue )
        {
            bluePositions [index] = pointData.position;
        }
        else
        {
            blackPositions [index] = pointData.position;
        }
        index++;
    }
    #endregion


    /******************************************************
     *                  UI OnClick Events
     ******************************************************/
    #region OnClick
    public void SetColorButton( int color )
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
        SetColor(newColor);
    }

    public void EraseAll()
    {
        allLines.Clear();
        RenderAllLines();
    }

    public void Undo()
    {
        if ( allLines.Count > 0 )
        {
            List<PointData> lastLine = allLines [allLines.Count - 1];
            allLines.RemoveAt(allLines.Count - 1);
        }
        RenderAllLines();
    }
    #endregion
}
