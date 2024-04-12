using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerMove mover;
    [SerializeField] public CamaraController camaraController;
    [SerializeField] GameObject image_Aim_Interactable;
    [SerializeField] GameObject image_Aim_UnInteractable;
    [SerializeField] public Transform ZoomedPos;
    [SerializeField] Light flash;
    [SerializeField] float interactRange = 100;

    public bool isInteract = false;

    private void Update()
    {
        mover.enabled = !isInteract;
        camaraController.enabled = !isInteract;
        if ( isInteract )
        {
            image_Aim_Interactable.SetActive(false);
            image_Aim_UnInteractable.SetActive(false);
            return;
        }

        if ( Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, interactRange) )
        {
            IInteractable interactable = hit.transform.gameObject.GetComponent<IInteractable>();
            if ( interactable == null )
            {
                image_Aim_Interactable.SetActive(false);
                image_Aim_UnInteractable.SetActive(true);
                return;
            }
            image_Aim_Interactable.SetActive(true);
            image_Aim_UnInteractable.SetActive(false);
            return;
        }
    }



    private void OnInteract( InputValue value )
    {
        if ( isInteract ) return;

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * interactRange, Color.red, 1.5f);
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

    private void OnFlash( InputValue value )
    {
        if ( value.isPressed )
        {
            flash.enabled = !flash.enabled;
        }
    }
}
