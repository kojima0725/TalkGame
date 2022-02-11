using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextChange : ITalkOrder
{
    public enum TargetWindow
    {
        MAIN,
        NAME
    }
    [SerializeField]
    TargetWindow targetWindow;
    [SerializeField, TextArea]
    string body;
    [SerializeField]
    bool isFadeIn;


    TextWindow textWindow;


    public TextChange() { }

    public TextChange(TargetWindow targetWindow, string body, bool isFadeIn)
    {
        this.targetWindow = targetWindow;
        this.body = body;
        this.isFadeIn = isFadeIn;
    }

    public bool Doing => textWindow?.Moving ?? false;

    public void Do(TalkManager talkManager)
    {
        switch (targetWindow)
        {
            case TargetWindow.MAIN:
                textWindow = talkManager.MainText;
                break;
            case TargetWindow.NAME:
                textWindow = talkManager.CharaNameText;
                break;
            default:
                break;
        }

        if (isFadeIn) { textWindow.SetNewText(body, talkManager.TextSpeed); }
        else { textWindow.SetNewText(body); }
        
    }

    public void UpdateDo()
    {
        //DoNothing;
    }

    public void RequestSkip()
    {
        textWindow.RequestSkip();
    }
}










/// <summary>
/// これはダミーです！！使用非推奨！！DoNotUseThis
/// for Unity Bug Fix
/// </summary>
[System.Serializable]
public class TextOrder : ITalkOrder
{
    public enum TargetWindow
    {
        MAIN,
        NAME
    }
    [Header("これはダミーです！！使用非推奨！！DoNotUseThis")]
    [SerializeField]
    TargetWindow targetWindow;
    [SerializeField]
    string body;
    [SerializeField]
    bool isFadeIn;


    TextWindow textWindow;

    public bool Doing => textWindow?.Moving ?? false;

    public void Do(TalkManager talkManager)
    {
        switch (targetWindow)
        {
            case TargetWindow.MAIN:
                textWindow = talkManager.MainText;
                break;
            case TargetWindow.NAME:
                textWindow = talkManager.CharaNameText;
                break;
            default:
                break;
        }

        if (isFadeIn) { textWindow.SetNewText(body, talkManager.TextSpeed); }
        else { textWindow.SetNewText(body); }

    }

    public void UpdateDo()
    {
        //DoNothing;
    }

    public void RequestSkip()
    {
        textWindow.RequestSkip();
    }
}
