using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TalkManagerが入力に応じてスキップを処理してくれるので今のところここでは入力を受け付けない
/// </summary>
public class WaitInputCtrl : ITalkOrder
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
