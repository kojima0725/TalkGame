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


    bool doing = true;
    public bool Doing => doing;

    public void Do(TalkManager talkManager)
    {
        ITalkOrder[] talkOrders = BuildOrders();
        TalkOrderProcesser processer = new TalkOrderProcesser(talkOrders, talkManager);
        processer.StartAllProcess(OnEndProcess);
    }

    public void RequestSkip()
    {
        //スキップ入力の受け取り、処理はprocesser内で行っている
        return;
    }

    public void UpdateDo()
    {
        return;
    }

    private void OnEndProcess()
    {
        doing = false;
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
