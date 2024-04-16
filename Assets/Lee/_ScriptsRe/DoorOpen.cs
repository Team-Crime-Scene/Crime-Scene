using System.Collections;
using UnityEngine;

public class DoorOpen : InteractableObject
{
    [SerializeField] Transform doorTrans;
    Quaternion doorinitialRotation;
    bool isOpen;

    private void Awake()
    {
        doorinitialRotation = doorTrans.rotation;
    }
    public override void Interact( PlayerController player )
    {
        if ( isOpen )
        {
            if ( openRoutine == null )
                openRoutine = StartCoroutine(doorOpenRoutine());
        }
        else
        {
            if ( closeRoutine == null )
                closeRoutine = StartCoroutine(doorClosRoutine());
        }
        UnInteract(player);
    }
    Coroutine openRoutine;
    IEnumerator doorOpenRoutine()
    {
        Debug.Log("코루틴에 들어옴");
        while ( doorTrans.rotation != Quaternion.Euler(-90, 90,0) )
        {
            Debug.Log("코루틴 조건에 들어옴");
            doorTrans.rotation = Quaternion.Lerp(doorTrans.rotation, Quaternion.Euler(-90, 90, 0), 0.5f);

            yield return new WaitForSeconds(0.02f); ;
        }
        if ( closeRoutine != null )
            StopCoroutine(closeRoutine);
        closeRoutine = null;
        isOpen = false;
    }

    Coroutine closeRoutine;
    IEnumerator doorClosRoutine()
    {
        while ( doorTrans.rotation != doorinitialRotation )
        {
            doorTrans.rotation = Quaternion.Lerp(doorTrans.rotation, doorinitialRotation, 0.5f);

            yield return new WaitForSeconds(0.02f);
        }
        if ( openRoutine != null )
            StopCoroutine(openRoutine);
        openRoutine = null;
        isOpen = true;
    }
    public override void UnInteract( PlayerController player )
    {
        player.isInteract = false;
    }
}
