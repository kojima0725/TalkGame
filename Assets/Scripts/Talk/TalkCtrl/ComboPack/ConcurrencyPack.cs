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

    WaitInput waitInput;
    bool processDoing = true;

    public bool Doing => waitInput.Doing;

    public void Do(TalkManager talkManager)
    {
        processDoing = true;
        TalkOrderProcesser processer = new TalkOrderProcesser(orders, talkManager);
        processer.StartAllProcess(() => processDoing = false);
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
        //DoNothing
    }
}
