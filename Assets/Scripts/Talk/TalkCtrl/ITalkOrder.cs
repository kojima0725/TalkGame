using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TalkOrderProcesserクラスが処理する
/// </summary>
public interface ITalkOrder
{
    /// <summary>
    /// 命令の開始時に一度だけ呼ばれる
    /// </summary>
    /// <param name="talkManager"></param>
    public void Do(TalkManager talkManager);
    /// <summary>
    /// 命令実行中、毎フレーム呼ばれる
    /// </summary>
    public void UpdateDo();
    /// <summary>
    /// プレイヤーからのスキップ入力などがあった場合に呼ばれる
    /// </summary>
    public void RequestSkip();
    /// <summary>
    /// 命令が実行中かどうかのフラグ
    /// </summary>
    public bool Doing { get; }
}
