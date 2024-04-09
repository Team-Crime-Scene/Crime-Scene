using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnhancedWhiteBoard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IDragHandler , IInteractable 
{
    //���� �׸� �� ���� ���ο� LineRender ��ü�� �����ϴ� ����

    // ���� : ���� ������ �����ϱ� ������. ���߿� ���� ��ü�� ���忡 �߰��Ҷ� SortingLayer ������ ���� ���� �� ����
    // ���� : Save&Load �� ���ϰ� ŭ
    [SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] LineRenderer linePrefab;
    [SerializeField] PopUpUI popUpUI;
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
     *             Interact  Interfaces
    ******************************************************/
    public void Interact( PlayerController interacter )
    {
        vCam.Priority = 100;
        interacter.transform.position = vCam.transform.position;
        Manager.UI.ShowPopUpUI(popUpUI);
    }

    public void UnInteract( PlayerController interacter )
    {
        Manager.UI.ClosePopUpUI();
        vCam.Priority = 0;
    }


    /******************************************************
     *                         ETC
     ******************************************************/

    private Vector3 GetLocalPosition( PointerEventData eventData ) //Extension�� Ȯ��޼���� �ۼ��ص�. ���߿� ���� ��
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
        //�Ű������� Enum�̸� OnClick�� ��� �Ұ�...
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

    //��� ���� ����
    public void EraseAll()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            Destroy(lines [i].gameObject);
        }
        lines.Clear();
    }

    //������ �׾��� ���� ����
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