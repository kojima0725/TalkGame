using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] backGrounds;
    int currentBG;

    public bool Moving()
    {
        //��X
        return false;
    }

    public void RequestSkip()
    {
        //��X
    }

    public void ChangeBG(int code, float fadeTime = 1)
    {
        if (code == currentBG)
        {
            return;
        }
        foreach (var item in backGrounds)
        {
            item.SetActive(false);
        }
        backGrounds[code].SetActive(true);
    }
}
