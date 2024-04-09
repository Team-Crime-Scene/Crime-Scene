using UnityEngine;
using UnityEngine.EventSystems;

public class MazePlay : MonoBehaviour, IDragHandler
{
    public float moveDistance = 1.0f; // �ѹ��� �̵��� �Ÿ�

    public void OnDrag( PointerEventData eventData )
    {
        Vector3 startPos = transform.position;
        Vector3 targetPosition = new Vector3(eventData.pointerCurrentRaycast.worldPosition.x, transform.position.y, eventData.pointerCurrentRaycast.worldPosition.z);
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize();

        RaycastHit hit;
        if ( Physics.Raycast(transform.position, direction, out hit, moveDistance) )
        {
            // ���� �浹�ϹǷ� �̵����� ����
            return;
        }
        if ( Vector3.Distance(targetPosition, startPos) > 1f )
            return;
        
        // ���� �浹���� �����Ƿ� �̵�
        transform.position = targetPosition;
    }
}