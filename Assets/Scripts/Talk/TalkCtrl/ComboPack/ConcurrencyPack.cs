using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 命令を同時に実行する
/// </summary>
public class ConcurrencyPack : ITalkOrder
{
    [SerializeReference, SubclassSelector]
    List<ITalkOrder> orders;
    bool processDoing = true;

    public bool Doing => processDoing;

    public ConcurrencyPack() { }

    public ConcurrencyPack(IEnumerable<ITalkOrder> orders)
    {
        this.orders = new List<ITalkOrder>(orders);
    }


    public void Do(TalkManager talkManager)
    {
        processDoing = true;
        TalkOrderProcesser processer = new TalkOrderProcesser(orders, talkManager);
        processer.StartAllProcess(() => processDoing = false);
    }

    public void RequestSkip()
    {
        //スキップ処理はTalkOrderProcesser内で行われている
        return;
    }

    public void UpdateDo()
    {
        //UpdateはTalkOrderProcesser内で行われている
        return;
    }
}
