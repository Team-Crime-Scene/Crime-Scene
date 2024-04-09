using System;
using System.Collections.Generic;
using UnityEngine.UI;


[Serializable]
public class Chapter1
{
    public List<string> PlayerSubAnswers1;
    public List<string> PlayerMultiAnswer;

    public Chapter1()
    {
        PlayerSubAnswers1 = new List<string>(10);
        PlayerMultiAnswer = new List<string>(10);
    }
}
[Serializable]
public class Chapter2
{


    public Chapter2()
    {

    }
}