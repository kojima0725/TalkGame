using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��b�V�[���̗�����������Ă����N���X
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
    /// ���߂��ォ�珇�ɏ�������
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
    /// ���ׂĂ̖��߂𓯎����s����
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
    /// ���ߎ��s(�ォ�珇��)
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateProcess()
    {
        while (orders.Count != 0)
        {
            if (!orders[0].Doing)
            {
                //���݂̖��߂��I��������폜���Ď��Ɉڂ�
                orders.RemoveAt(0);
                if (orders.Count != 0){ orders[0]?.Do(manager); }
            }
            else
            {
                //���݂̖��߂��X�V����
                orders[0].UpdateDo();
            }

            if (GetSkipInput() && (orders[0]?.Doing ?? false))
            {
                //�X�L�b�v���͂��󂯎������A���݂̖��߂ɃX�L�b�v�����N�G�X�g����
                orders[0].RequestSkip();
            }
            yield return null;
        }

        onProcessEnd?.Invoke();
    }

    /// <summary>
    /// ���ߎ��s(�S�ē�����)
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateAllProcess()
    {
        while (orders.Count != 0)
        {
            //���������������͍̂폜���Ă���
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
