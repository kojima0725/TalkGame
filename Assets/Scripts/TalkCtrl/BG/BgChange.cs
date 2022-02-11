using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BgChange : ITalkOrder
{
    [SerializeField]
    int changeTo;
    [SerializeField]
    bool isFade;

    BackGroundManager manager;

    public bool Doing => manager?.Moving ?? false;

    public void Do(TalkManager talkManager)
    {
        manager = talkManager.BgManager;
        if (isFade)
        {
            manager.ChangeBG(changeTo, talkManager.BackgroundFadeTime);
        }
        else
        {
            manager.ChangeBG(changeTo);
        }
        manager.ChangeBG(changeTo);
    }

    public void RequestSkip()
    {
        manager?.RequestSkip();
    }

    public void UpdateDo()
    {
        //DoNothing
    }
}
