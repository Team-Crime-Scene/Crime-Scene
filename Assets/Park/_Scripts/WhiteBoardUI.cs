using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteBoardUI : PopUpUI
{
    private EnhancedWhiteBoard whiteBoard;

    protected override void Awake()
    {
        base.Awake();
        whiteBoard = FindAnyObjectByType<EnhancedWhiteBoard>();

        GetUI<Button>("BTN_SetColor(Black)").onClick.AddListener(()=>whiteBoard.SetColorButton(0));
        GetUI<Button>("BTN_SetColor(Red)").onClick.AddListener(()=>whiteBoard.SetColorButton(1));
        GetUI<Button>("BTN_SetColor(Blue)").onClick.AddListener(()=>whiteBoard.SetColorButton(2));
        GetUI<Button>("BTN_EraseAll").onClick.AddListener(whiteBoard.EraseAll);
        GetUI<Button>("BTN_Undo").onClick.AddListener(whiteBoard.Undo);
    }
}
