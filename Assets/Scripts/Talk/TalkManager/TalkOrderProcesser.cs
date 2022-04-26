using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 会話シーンの流れを処理していくクラス
/// </summary>
public class TalkOrderProcesser
{
    List<ITalkOrder> orders;
    TalkManager manager;
    System.Action onProcessEnd;

    public TalkOrderProcesser(IEnumerable<ITalkOrder> orders, TalkManager manager)
    {
        this.orders = new List<ITalkOrder>(orders);
        this.manager = manager;
    }

    /// <summary>
    /// 命令を上から順に処理する
    /// </summary>
    /// <param name="onProcessEnd"></param>
    public void StartProcess(System.Action onProcessEnd = null)
    {
        orders[0].Do(manager);
        manager.DoCoroutine(UpdateProcess);
        if (onProcessEnd != null)
        {
            this.onProcessEnd += onProcessEnd;
        }
    }

    /// <summary>
    /// すべての命令を同時実行する
    /// </summary>
    /// <param name="onProcessEnd"></param>
    public void StartAllProcess(System.Action onProcessEnd = null)
    {
        foreach (var order in orders)
        {
            order.Do(manager);
        }
        manager.DoCoroutine(UpdateAllProcess);
        if (onProcessEnd != null)
        {
            this.onProcessEnd += onProcessEnd;
        }
    }

    /// <summary>
    /// 命令実行(上から順に)
    /// </summary>
    /// <returns></returns>
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

        onProcessEnd?.Invoke();
    }

    /// <summary>
    /// 命令実行(全て同時に)
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateAllProcess()
    {
        while (orders.Count != 0)
        {
            //処理完了したものは削除しておく
            orders.RemoveAll(x => !x.Doing);

            //Update
            if (GetSkipInput())
            {
                foreach (var order in orders)
                {
                    order.UpdateDo();
                    order.RequestSkip();
                }
            }
            else
            {
                foreach (var order in orders)
                {
                    order.UpdateDo();
                }
            }
            yield return null;
        }

        onProcessEnd?.Invoke();
    }


    private bool GetSkipInput()
    {
        return manager.SkipInput;
    }
}
