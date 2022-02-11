using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TalkOrderProcesserが入力に応じてスキップを処理してくれるので今のところここでは入力を管理していない
/// </summary>
public class WaitInput : ITalkOrder
{
    bool moving = true;

    public bool Doing => moving;

    public void Do(TalkManager talkManager)
    {
        moving = true;
    }

    public void RequestSkip()
    {
        moving = false;
    }

    public void UpdateDo()
    {
        //DoNothing
    }
}
