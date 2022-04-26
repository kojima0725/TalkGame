using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;


/// <summary>
/// ‰ï˜bƒV[ƒ“‚ğŠÇ—‚·‚éƒNƒ‰ƒX
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
    /// ï¿½Xï¿½Lï¿½bï¿½vï¿½ï¿½ï¿½Í‚ÍAï¿½ï¿½ï¿½oï¿½ï¿½ï¿½ï¿½ï¿½Éƒtï¿½ï¿½ï¿½Oï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Ì‚Å’ï¿½ï¿½ï¿½
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
        // Addressables‚É‚æ‚é“Ç‚İ‚İ
        Addressables.LoadAssetAsync<TextAsset>(fileName).Completed += handle =>
        {
            string json = handle.Result.ToString();
            //ƒfƒVƒŠƒAƒ‰ƒCƒY
            if (string.IsNullOrEmpty(json)) return;
            orders = JsonUtility.FromJson<TalkOrderList>(json).order;
            //–½—ßÀs
            TalkOrderProcesser processer = new TalkOrderProcesser(orders, this);
            processer.StartProcess(EndTalk);
            Debug.Log($"V‚µ‚¢ƒVƒiƒŠƒI{fileName}‚ğÀsŠJn‚µ‚Ü‚µ‚½");
        };
    }

    private void EndTalk()
    {
        Debug.Log("ƒVƒiƒŠƒI‚ªI—¹‚µ‚Ü‚µ‚½");
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
