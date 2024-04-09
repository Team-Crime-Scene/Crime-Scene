using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInteractController : MonoBehaviour
{
    [SerializeField] float interactRange = 100;


    public bool isInteract = false;

    public void OnInteract( InputValue value )
    {
        Debug.Log("Player OnInteract");
        //Raycast ���� transform ���� �ƴ϶� ī�޶� �������� �ٲܰ�
        if ( Physics.Raycast(transform.position, Camera.main.transform.forward, out RaycastHit hit, interactRange) )
        {
            IInteractable interactable = hit.transform.gameObject.GetComponent<IInteractable>();
            if ( interactable == null ){
                isInteract = false;
                return;
            }
            isInteract = true;
            Manager.Game.Interaction(interactable);
        }
    }
}
