using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���߂𓯎��Ɏ��s����
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
        //�X�L�b�v������TalkOrderProcesser���ōs���Ă���
        return;
    }

    public void UpdateDo()
    {
        //Update��TalkOrderProcesser���ōs���Ă���
        return;
    }
}
