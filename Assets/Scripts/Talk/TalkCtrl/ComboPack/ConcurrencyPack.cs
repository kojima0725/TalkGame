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

    WaitInput waitInput = new WaitInput();
    bool processDoing = true;

    public bool Doing => waitInput.Doing;

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
        waitInput.Do(talkManager);
    }

    public void RequestSkip()
    {
        //�������s���̃X�L�b�v���͂̎󂯎��A������processer���ōs���Ă���
        //�������s�^�X�N���������Ă���Ȃ�A�X�L�b�v���߂��󂯎��A��������
        if (!processDoing)
        {
            waitInput.RequestSkip();
        }
        return;
    }

    public void UpdateDo()
    {
        if (!processDoing)
        {
            waitInput.UpdateDo();
        }
        return;
    }
}
