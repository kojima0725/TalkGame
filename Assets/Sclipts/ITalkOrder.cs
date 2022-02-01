using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���̏��K�v�Ȃ�
//public enum OrderType
//{
//    ChangeBG,
//    Talk,
//    ShowChara,
//    HIdeChara,
//    WaitInput,
//}

/// <summary>
/// TalkManager���g�p����
/// </summary>
public interface ITalkOrder
{
    public void Do(TalkManager talkManager);
    public void UpdateDo();
    public void RequestSkip();
    public bool Doing { get; }
}
