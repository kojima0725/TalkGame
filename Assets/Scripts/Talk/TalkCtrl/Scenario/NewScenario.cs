using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewScenario : ITalkOrder
{
    [SerializeField]
    string scenarioName;

    bool doing = true;
    public bool Doing => doing;

    public NewScenario() { }

    public NewScenario(string scenarioName)
    {
        this.scenarioName = scenarioName;
    }

    public void Do(TalkManager talkManager)
    {
        talkManager.NewTalkFromJson(scenarioName);
        doing = false;
    }

    public void RequestSkip()
    {
        //‰½‚à‚µ‚È‚¢
    }

    public void UpdateDo()
    {
        //‰½‚à‚µ‚È‚¢
    }
}
