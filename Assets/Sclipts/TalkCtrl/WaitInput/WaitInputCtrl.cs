using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TalkManager�����͂ɉ����ăX�L�b�v���������Ă����̂ō��̂Ƃ��낱���ł͓��͂��󂯕t���Ȃ�
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
