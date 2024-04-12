using System;

[Serializable]

public class GameData
{
    public Tutorial tutorialData;
    public Chapter1 chapter1Data;

    public GameData()
    {
        chapter1Data = new Chapter1();
        tutorialData = new Tutorial();
    }
}
