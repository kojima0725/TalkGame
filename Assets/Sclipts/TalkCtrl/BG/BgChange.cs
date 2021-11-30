using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgChange : ITalkOrder
{
    BackGroundManager manager;

    public bool Doing => manager.Moving;

    public void Do(TalkManager talkManager)
    {
        throw new System.NotImplementedException();
    }

    public void RequestSkip()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateDo()
    {
        throw new System.NotImplementedException();
    }
}
