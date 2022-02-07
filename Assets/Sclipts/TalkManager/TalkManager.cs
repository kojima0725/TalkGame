using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TalkManager : MonoBehaviour
{
    [SerializeReference, SubclassSelector]
    List<ITalkOrder> orders;

    [SerializeField]
    TextWindow mainText;
    [SerializeField]
    TextWindow charaNameText;
    [SerializeField]
    BackGroundManager bgManager;
    [SerializeField]
    CharaManager charaManager;
    [SerializeField]
    float textSpeed;
    [SerializeField]
    float bgFadeTime;


    public TextWindow MainText => mainText;
    public TextWindow CharaNameText => charaNameText;
    public BackGroundManager BgManager => bgManager;
    public CharaManager CharaManager => charaManager;
    public float TextSpeed => textSpeed;
    public float BackgroundFadeTime => bgFadeTime;


    private void Start()
    {
        orders[0].Do(this);
    }

    private void Update()
    {
        //���߂��Ȃ��Ȃ牽�����Ȃ�
        if (orders.Count == 0)
        {
            return;
        }

        if (!orders[0].Doing)
        {
            //���݂̖��߂��I��������폜���Ď��Ɉڂ�
            orders.RemoveAt(0);
            if (orders.Count != 0)
            {
                orders[0].Do(this);
            }
        }
        else
        {
            //���݂̖��߂��X�V����
            orders[0].UpdateDo();
        }

        if (GetSkipInput())
        {
            //�X�L�b�v���͂��󂯎������A���݂̖��߂ɃX�L�b�v�����N�G�X�g����
            if (orders[0]?.Doing ?? false)
            {
                orders[0].RequestSkip();
            }
        }
        
    }

    private bool GetSkipInput()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }



#if UNITY_EDITOR
    public List<ITalkOrder> TalkOrders { get => orders; set => orders = value; }
#endif
}
