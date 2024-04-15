using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : InteractableObject
{
    [SerializeField] Animator animator;
    public override void Interact( PlayerController player )
    {
        animator.Play("Closet_Open");
    }
    public override void UnInteract( PlayerController player )
    {
        animator.Play("Closet_Close");
    }
}
