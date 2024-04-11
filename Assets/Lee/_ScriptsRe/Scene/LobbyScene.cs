using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
    public void Tutorial()
    {
        Manager.Scene.LoadScene("LeeTutorialScene");
    }

}
