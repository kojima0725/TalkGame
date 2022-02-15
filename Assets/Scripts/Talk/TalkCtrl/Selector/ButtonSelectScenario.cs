using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSelectScenario : ITalkOrder
{
    [SerializeField]
    List<ButtonNameAndScenario> choices;

    bool doing = true;
    public bool Doing => doing;

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

    private void Selected(ButtonNameAndScenario choice, TalkManager talkManager)
    {
        NewScenario newScenario = new NewScenario(choice.Scenario);
        newScenario.Do(talkManager);
        doing = false;
    }

    [System.Serializable]
    public class ButtonNameAndScenario
    {
        [SerializeField]
        string buttonName;
        [SerializeField]
        string scenario;

        public string Name => buttonName;
        public string Scenario => scenario;
    }
}
