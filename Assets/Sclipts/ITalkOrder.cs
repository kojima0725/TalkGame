using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TalkOrderProcesserクラスが処理する
/// </summary>
public interface ITalkOrder
{
    public void Do(TalkManager talkManager);
    public void UpdateDo();
    public void RequestSkip();
    public bool Doing { get; }
}
