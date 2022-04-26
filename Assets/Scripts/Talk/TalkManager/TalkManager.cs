using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;


/// <summary>
/// ��b�V�[�����Ǘ�����N���X
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
    /// �X�L�b�v���͂́A���o�����Ƀt���O��������̂Œ���
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

    private void Update()
    {
        UpdateSkipInput();
    }

    /// <summary>
    /// json�t�@�C������V�i���I�𐶐����A�V���ɏ������J�n����
    /// </summary>
    /// <param name="fileName"></param>
    public void NewTalkFromJson(string fileName)
    {
        string json = Resources.Load<TextAsset>(fileName).ToString();


        //�f�V���A���C�Y
        if (string.IsNullOrEmpty(json)) return;
        orders = JsonUtility.FromJson<TalkOrderList>(json).order;
        //���ߎ��s
        TalkOrderProcesser processer = new TalkOrderProcesser(orders, this);
        processer.StartProcess(EndTalk);
        Debug.Log($"�V�����V�i���I{fileName}�����s�J�n���܂���");

    }

    public void StopAllProcess()
    {
        StopAllCoroutines();
    }

    private void EndTalk()
    {
        Debug.Log("�V�i���I���I�����܂���");
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
