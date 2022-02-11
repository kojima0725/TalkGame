using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField]
    Image[] backGrounds;
    int currentBG;

    bool moving;

    public bool Moving => moving;

    public void RequestSkip()
    {
        //今はスキップを受け付けないので何もしない
    }

    public void ChangeBG(int code)
    {
        if (code == currentBG)
        {
            return;
        }
        foreach (var item in backGrounds)
        {
            item.gameObject.SetActive(false);
        }
        backGrounds[code].gameObject.SetActive(true);
        currentBG = code;
    }

    public void ChangeBG(int code, float fadeTime)
    {
        if (code == currentBG)
        {
            return;
        }
        currentBG = code;
        StartCoroutine(BGChange(backGrounds[code], fadeTime));
    }

    IEnumerator BGChange(Image fadein, float time)
    {
        moving = true;
        float timer = 0;
        fadein.transform.SetAsLastSibling();
        fadein.gameObject.SetActive(true);

        //フェードインはじめ
        {
            Color color = fadein.color;
            color.a = 0;
            fadein.color = color;
            yield return null;
        }
        //フェードイン中
        while (timer < time)
        {
            timer += Time.deltaTime;
            Color color = fadein.color;
            color.a = timer / time;
            fadein.color = color;
            yield return null;
        }
        //フェードイン終わり
        {
            Color color = fadein.color;
            color.a = 1;
            fadein.color = color;
        }
        moving = false;
    }
}
