using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TalkOrderProcesser�N���X����������
/// </summary>
public interface ITalkOrder
{
    /// <summary>
    /// ���߂̊J�n���Ɉ�x�����Ă΂��
    /// </summary>
    /// <param name="talkManager"></param>
    public void Do(TalkManager talkManager);
    /// <summary>
    /// ���ߎ��s���A���t���[���Ă΂��
    /// </summary>
    public void UpdateDo();
    /// <summary>
    /// �v���C���[����̃X�L�b�v���͂Ȃǂ��������ꍇ�ɌĂ΂��
    /// </summary>
    public void RequestSkip();
    /// <summary>
    /// ���߂����s�����ǂ����̃t���O
    /// </summary>
    public bool Doing { get; }
}
