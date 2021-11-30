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
        //���̓X�L�b�v���󂯕t���Ȃ��̂ŉ������Ȃ�
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

        //�t�F�[�h�C���͂���
        {
            Color color = fadein.color;
            color.a = 0;
            fadein.color = color;
            yield return null;
        }
        //�t�F�[�h�C����
        while (timer < time)
        {
            timer += Time.deltaTime;
            Color color = fadein.color;
            color.a = timer / time;
            fadein.color = color;
            yield return null;
        }
        //�t�F�[�h�C���I���
        {
            Color color = fadein.color;
            color.a = 1;
            fadein.color = color;
        }
        moving = false;
    }
}
