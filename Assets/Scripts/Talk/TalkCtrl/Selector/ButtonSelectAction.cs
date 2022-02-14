using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelectAction : ITalkOrder
{
    [SerializeField]
    List<ButtonNameAndAction> choices;

    bool doing = true;
    public bool Doing => doing;

    TalkOrderProcesser processer;

    public void Do(TalkManager talkManager)
    {
        SelectButtonManager manager = talkManager.SelectButtonManager;
        foreach (var choice in choices)
        {
            manager.SetNewButton(choice.Name, () => Selected(choice, talkManager));
        }
    }

    public void RequestSkip()
    {
        //スキップ不可
    }

    public void UpdateDo()
    {
        //何もしない
    }

    private void Selected(ButtonNameAndAction choice, TalkManager talkManager)
    {
        processer = new TalkOrderProcesser(choice.Action, talkManager);
        processer.StartProcess(() => doing = false);
    }

    [System.Serializable]
    public class ButtonNameAndAction
    {
        [SerializeField]
        string name;
        [SerializeReference, SubclassSelector]
        List<ITalkOrder> action;

        public string Name => name;
        public List<ITalkOrder> Action => action;
    }
}


