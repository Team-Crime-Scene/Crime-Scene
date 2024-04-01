using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField] LayerMask layerMask;
   public void OnInteraction( InputValue value )
    {
        Vector3 origin = Camera.main.ViewportToWorldPoint(playerCamera.transform.position);
        RaycastHit hit;
        Vector3 rayDir = playerCamera.transform.forward;
        if (Physics.Raycast(origin, rayDir, out hit, 10, layerMask) )
        {
            Debug.Log(hit.transform.gameObject.name);
        }

    }
}
