using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefatoringScene : BaseScene
{
    [SerializeField] Transform player;

    public override IEnumerator LoadingRoutine()
    {
        Manager.Data.LoadData();

        yield return null;


    }

}
