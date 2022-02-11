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
        // Addressables�ɂ��ǂݍ���
        Addressables.LoadAssetAsync<TextAsset>(fileName).Completed += handle =>
        {
            string json = handle.Result.ToString();
            //�f�V���A���C�Y
            if (string.IsNullOrEmpty(json)) return;
            orders = JsonUtility.FromJson<TalkOrderList>(json).order;
            //���ߎ��s
            TalkOrderProcesser processer = new TalkOrderProcesser(orders, this);
            processer.StartProcess(EndTalk);
            Debug.Log("�V�����V�i���I�����s�J�n���܂���");
        };
    }

    private void EndTalk()
    {
        Debug.Log("��b�V�[�����I�����܂���");
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
