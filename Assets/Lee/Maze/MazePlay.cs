using UnityEngine;
using UnityEngine.EventSystems;

public class MazePlay : MonoBehaviour, IDragHandler
{
    public float moveDistance = 1.0f; // 한번에 이동할 거리

    public void OnDrag( PointerEventData eventData )
    {
        Vector3 startPos = transform.position;
        Vector3 targetPosition = new Vector3(eventData.pointerCurrentRaycast.worldPosition.x, transform.position.y, eventData.pointerCurrentRaycast.worldPosition.z);
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize();

        RaycastHit hit;
        if ( Physics.Raycast(transform.position, direction, out hit, moveDistance) )
        {
            // 벽과 충돌하므로 이동하지 않음
            return;
        }
        if ( Vector3.Distance(targetPosition, startPos) > 1f )
            return;
        
        // 벽과 충돌하지 않으므로 이동
        transform.position = targetPosition;
    }
}