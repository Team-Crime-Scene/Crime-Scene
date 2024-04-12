using System;

[Serializable]

public class GameData
{
    public TutorialData tutorialData;
    public Chapter1Data chapter1Data;

    public GameData()
    {
        chapter1Data = new Chapter1Data();
        tutorialData = new TutorialData();
    }
}
