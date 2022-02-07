using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �ŏ���StartProcess���Ăяo���Ă���A���t���[��UpdateProcess���ĂԂ���
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
    }

    private bool GetSkipInput()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }
}
