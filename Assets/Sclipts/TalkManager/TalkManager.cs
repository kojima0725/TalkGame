using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TalkManager : MonoBehaviour
{
    #region PRIVATE
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

    #endregion

    #region PUBLIC
    public TextWindow MainText => mainText;
    public TextWindow CharaNameText => charaNameText;
    public BackGroundManager BgManager => bgManager;
    public CharaManager CharaManager => charaManager;
    public float TextSpeed => textSpeed;
    public float BackgroundFadeTime => bgFadeTime;
    public void DoCoroutine(System.Func<IEnumerator> c) => StartCoroutine(c());
    #endregion


    private void Start()
    {
        TalkOrderProcesser processer = new TalkOrderProcesser(orders, this);
        processer.StartProcess(EndTalk);
    }

    private void EndTalk()
    {
        Debug.Log("会話シーンが終了しました");
    }


#if UNITY_EDITOR
    public List<ITalkOrder> TalkOrders { get => orders; set => orders = value; }
#endif
}
