using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//今の所必要なし
//public enum OrderType
//{
//    ChangeBG,
//    Talk,
//    ShowChara,
//    HIdeChara,
//    WaitInput,
//}

/// <summary>
/// TalkManagerが使用する
/// </summary>
public interface ITalkOrder
{
    public void Do(TalkManager talkManager);
    public void UpdateDo();
    public void RequestSkip();
    public bool Doing { get; }
}
