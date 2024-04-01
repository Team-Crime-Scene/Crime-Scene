using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform zoomPos;
    private Vector3 curObject;
    private Quaternion rotate;
    // 실험용
    bool zoomObject = false;

    public void OnInteraction( InputValue value )
    {
        Vector3 origin = playerCamera.position;
        RaycastHit hit;
        Vector3 rayDir = playerCamera.transform.forward;

        Vector3 zoom = zoomPos.position;

        if ( Physics.Raycast(origin, rayDir, out hit, 100, layerMask) )
        {
            if ( zoomObject == false )
            {
                rotate = hit.transform.rotation;
                // 오브젝트의 처음 위치를 저장
                curObject = hit.transform.position;
                Vector3 dir = transform.position - curObject;
                hit.transform.rotation =  Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 2f);

                hit.transform.position = Vector3.Lerp(zoom, hit.transform.position, Time.deltaTime * 2f);

                zoomObject = true;
            }
            else
            {
                // 나중에 q또는 esc로 바꿀 예정
                hit.transform.position = Vector3.Lerp(curObject, zoom, Time.deltaTime * 2f);
                hit.transform.rotation = rotate;
                zoomObject = false;
            }
        }

    }

    private void Update()
    {

    }
}
