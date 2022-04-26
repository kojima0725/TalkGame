using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;


/// <summary>
/// 会話シーンを管理するクラス
/// </summary>
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
    SelectButtonManager selectButtonManager;
    [SerializeField]
    float textSpeed;
    [SerializeField]
    float bgFadeTime;

    bool skipInput;

    #endregion

    #region PUBLIC
    public TextWindow MainText => mainText;
    public TextWindow CharaNameText => charaNameText;
    public BackGroundManager BgManager => bgManager;
    public CharaManager CharaManager => charaManager;
    public SelectButtonManager SelectButtonManager => selectButtonManager;
    public float TextSpeed => textSpeed;
    public float BackgroundFadeTime => bgFadeTime;
    public void DoCoroutine(System.Func<IEnumerator> c) => StartCoroutine(c());
    /// <summary>
    /// �ｿｽX�ｿｽL�ｿｽb�ｿｽv�ｿｽ�ｿｽ�ｿｽﾍは、�ｿｽ�ｿｽ�ｿｽo�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽﾉフ�ｿｽ�ｿｽ�ｿｽO�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽﾌで抵ｿｽ�ｿｽ�ｿｽ
    /// </summary>
    public bool SkipInput
    {
        get
        {
            bool r = skipInput;
            skipInput = false;
            return r;
        }
    }
    #endregion


    private void Start()
    {
        TalkOrderProcesser processer = new TalkOrderProcesser(orders, this);
        processer.StartProcess(EndTalk);
    }

    public void NewTalkFromJson(string fileName)
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
            Debug.Log($"新しいシナリオ{fileName}を実行開始しました");
        };
    }

    private void EndTalk()
    {
        Debug.Log("シナリオが終了しました");
    }

    private void UpdateSkipInput()
    {
        skipInput = false;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            skipInput = true;
        }
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
