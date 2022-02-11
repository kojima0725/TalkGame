using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaShow : ITalkOrder
{
    [SerializeField]
    int charaId;

    public CharaShow(){ }

    public CharaShow(int charaId)
    {
        this.charaId = charaId;
    }


    public bool Doing => false;

    public void Do(TalkManager talkManager)
    {
        talkManager.CharaManager.ShowChara(charaId);
    }

    public void RequestSkip()
    {
        return;
    }

    public void UpdateDo()
    {
        return;
    }
}
