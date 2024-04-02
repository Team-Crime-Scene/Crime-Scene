using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public static class Extension
{
    public static bool Contain(this LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }

    public static Vector3 GetLocalPosition (this PointerEventData eventData, Transform transform)
    {
        Vector3 worldPosition = eventData.pointerCurrentRaycast.worldPosition;
        return transform.InverseTransformPoint(worldPosition); 
    }
}