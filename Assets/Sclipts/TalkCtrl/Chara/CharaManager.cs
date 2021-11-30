using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaManager : MonoBehaviour
{
    [SerializeField]
    Charactor[] charactors;

    public bool Doing => false;//å„ÅX

    public void RequestSkip()
    {
        //å„ÅX
    }

    public void ShowChara(int chara, int pose = 0)
    {
        charactors[chara].gameObject.SetActive(true);
        charactors[chara].ChangePose(pose);
    }

    public void ChangeCharaPose(int chara, int pose)
    {
        charactors[chara].ChangePose(pose);
    }

    public void HideChara(int chara)
    {
        charactors[chara].gameObject.SetActive(false);
    }
}
