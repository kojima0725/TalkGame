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
    
    WaitInput waitInput = new WaitInput();
    bool processDoing = true;

    public bool Doing => waitInput.Doing;

    public void Do(TalkManager talkManager)
    {
        ITalkOrder[] talkOrders = BuildOrders();
        TalkOrderProcesser processer = new TalkOrderProcesser(talkOrders, talkManager);
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

    ITalkOrder[] BuildOrders()
    {
        ITalkOrder[] a = new ITalkOrder[3];

        a[0] = new CharaShow(charaId);
        a[1] = new TextChange(TextChange.TargetWindow.NAME, charaName, false);
        a[2] = new TextChange(TextChange.TargetWindow.MAIN, talkBody, true);

        return a;
    }
}
