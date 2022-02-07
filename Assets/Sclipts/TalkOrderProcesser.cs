using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 最初にStartProcessを呼び出してから、毎フレームUpdateProcessを呼ぶこと
/// </summary>
public class TalkOrderProcesser
{
    List<ITalkOrder> orders;
    TalkManager manager;

    public bool Doing => orders.Count > 0;

    public TalkOrderProcesser(IEnumerable<ITalkOrder> orders, TalkManager manager)
    {
        this.orders = new List<ITalkOrder>(orders);
        this.manager = manager;
    }

    public void StartProcess()
    {
        orders[0].Do(manager);
        manager.DoCoroutine(UpdateProcess);
    }

    private IEnumerator UpdateProcess()
    {
        while (orders.Count != 0)
        {
            if (!orders[0].Doing)
            {
                //現在の命令が終了したら削除して次に移る
                orders.RemoveAt(0);
                if (orders.Count != 0){ orders[0]?.Do(manager); }
            }
            else
            {
                //現在の命令を更新する
                orders[0].UpdateDo();
            }

            if (GetSkipInput() && (orders[0]?.Doing ?? false))
            {
                //スキップ入力を受け取ったら、現在の命令にスキップをリクエストする
                orders[0].RequestSkip();
            }
            yield return null;
        }
    }

    private bool GetSkipInput()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }
}
