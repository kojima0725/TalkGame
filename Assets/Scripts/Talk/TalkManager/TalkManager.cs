using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
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

    private void NewTalkFromJson(string fileName)
    {
        // Addressablesによる読み込み
        Addressables.LoadAssetAsync<TextAsset>(fileName).Completed += handle =>
        {
            string json = handle.Result.ToString();
            //デシリアライズ
            if (string.IsNullOrEmpty(json)) return;
            orders = JsonUtility.FromJson<TalkOrderList>(json).order;
            //命令実行
            TalkOrderProcesser processer = new TalkOrderProcesser(orders, this);
            processer.StartProcess(EndTalk);
            Debug.Log("新しいシナリオを実行開始しました");
        };
    }

    private void EndTalk()
    {
        Debug.Log("会話シーンが終了しました");
    }


#if UNITY_EDITOR
    public List<ITalkOrder> TalkOrders { get => orders; set => orders = value; }

    [ContextMenu("NewTalkFromJsonTest")]
    public void NewTalkFromJsonTest()
    {
        NewTalkFromJson("TalkData");
    }
#endif
}

[System.Serializable]
public class TalkOrderList
{
    [SerializeReference]
    public List<ITalkOrder> order;
}
