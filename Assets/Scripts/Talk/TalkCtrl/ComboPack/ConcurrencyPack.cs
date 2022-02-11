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
        //同時実行内のスキップ入力の受け取り、処理はprocesser内で行っている
        //同時実行タスクが完了しているなら、スキップ命令を受け取り、処理する
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
