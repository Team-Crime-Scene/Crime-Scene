using UnityEngine;
using UnityEngine.EventSystems;

public class MazePlay : MonoBehaviour, IDragHandler
{
    public void OnDrag( PointerEventData eventData )
    {
        transform.position = new Vector3(eventData.pointerCurrentRaycast.worldPosition.x, transform.position.y, eventData.pointerCurrentRaycast.worldPosition.z);
    }
}