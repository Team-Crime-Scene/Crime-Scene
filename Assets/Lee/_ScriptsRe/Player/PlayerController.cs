using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerMove mover;
    [SerializeField] CamaraController camaraController;
    [SerializeField] Image aimedImage;
    [SerializeField] public Transform ZoomedPos; 
    [SerializeField] Light flash;
    [SerializeField] float interactRange = 100;

    public bool isInteract = false;

    private void Update()
    {
        mover.enabled = !isInteract;
        camaraController.enabled = !isInteract;
        aimedImage.enabled = !isInteract; // 나중에 이벤트로
    }


    private void OnInteract( InputValue value )
    {
        if ( isInteract ) return;

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactRange, Color.red,1.5f);
        if ( Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, interactRange) )
        {
            IInteractable interactable = hit.transform.gameObject.GetComponent<IInteractable>();
            if ( interactable == null )
            {
                return;
            }
            Manager.Game.Interaction(interactable);
        }
    }

    private void OnFlash(InputValue value )
    {
        if ( value.isPressed )
        {
            flash.enabled = !flash.enabled;
        }
    }
       
    
}
