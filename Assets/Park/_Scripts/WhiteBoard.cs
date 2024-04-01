using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WhiteBoard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler ,IPointerExitHandler
{
    [SerializeField] LineRenderer lineRendererBlack;
    [SerializeField] LineRenderer lineRendererRed;
    [SerializeField] LineRenderer lineRendererBlue;

    [SerializeField] float brushSize = 0.03f;

    private List<List<PointData>> allLines = new List<List<PointData>>(); // 모든 선 
    private List<PointData> currentLine = new List<PointData>(); //현재 그리는 선 

    private bool isDrawing = false;
    [SerializeField] Color currentColor = Color.black;
    private int positionCount = 2;

    void Start()
    {
        InitLineRenderer();
    }
    /******************************************************
     *          Pointer Down/Up & Drag Interface
     ******************************************************/
    #region Interfaces

    public void OnPointerDown( PointerEventData eventData )
    {
        isDrawing = true;
        currentLine.Clear();


        // AddPoint(eventData.pointerCurrentRaycast.worldPosition); // LineRender 초기 위치 부터 시작해서 삐죽삐죽함
        Vector3 downPos = GetLocalPosition(eventData);
        AddPoint(new Vector3(downPos.x, downPos.y, 0)); // 모든 선이 다 그려짐
        RenderAllLines();
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

    public void OnDrag( PointerEventData eventData )
    {

        if ( isDrawing )
        {
            AddPoint(GetLocalPosition(eventData)); // 범위 안에 있을 때만 추가하도록 수정해야함
            RenderAllLines();
        }
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

    private Vector3 GetLocalPosition( PointerEventData eventData )
    {
        Vector3 worldPosition = eventData.pointerCurrentRaycast.worldPosition;
        return transform.InverseTransformPoint(worldPosition);
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
    private void RenderCurrentLine() //RenderAllLines에 병합됨
    {
        // lineRenderer.positionCount = currentLine.Count;
        // lineRenderer.SetPositions(currentLine.ToArray());
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
