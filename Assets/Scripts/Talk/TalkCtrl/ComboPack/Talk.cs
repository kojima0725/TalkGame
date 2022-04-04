using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : ITalkOrder
{
    [SerializeField]
    int charaId;
    [SerializeField]
    string charaName;
    [SerializeField, TextArea]
    string talkBody;

    ConcurrencyPack pack;

    public bool Doing => pack?.Doing ?? true;

    public void Do(TalkManager talkManager)
    {
        ITalkOrder[] talkOrders = BuildOrders();
        pack = new ConcurrencyPack(talkOrders);
        pack.Do(talkManager);
    }

    public void RequestSkip()
    {
        pack.RequestSkip();
        return;
    }

    public void UpdateDo()
    {
        pack?.UpdateDo();
        return;
    }

    ITalkOrder[] BuildOrders()
    {
        ITalkOrder[] a = new ITalkOrder[4];

        a[0] = new CharaShow(charaId);
        a[1] = new TextChange(TextChange.TargetWindow.NAME, charaName, false);
        a[2] = new TextChange(TextChange.TargetWindow.MAIN, talkBody, true);
        a[3] = new WaitInput();

        return a;
    }
}
