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
        //命令がないなら何もしない
        if (orders.Count == 0)
        {
            return;
        }

        if (!orders[0].Doing)
        {
            //現在の命令が終了したら削除して次に移る
            orders.RemoveAt(0);
            if (orders.Count != 0)
            {
                orders[0].Do(this);
            }
        }
        else
        {
            //現在の命令を更新する
            orders[0].UpdateDo();
        }

        if (GetSkipInput())
        {
            //スキップ入力を受け取ったら、現在の命令にスキップをリクエストする
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
