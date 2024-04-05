using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionManager : Singleton<InteractionManager>
{
    [SerializeField] PlayerController player;
    [SerializeField] GameObject interactObject;

     void Start()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("Player");
        player = gameObject.GetComponent<PlayerController>();
    }


    public void InterAction()
    {
        player.isInteract = true;
    }


    public void OnCancel()
    {
        player.isInteract = false;
    }
}
