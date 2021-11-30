using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextWindow : MonoBehaviour
{
    Text textBody;
    bool moving;
    bool skip;

    public bool Moving => moving;

    public void RequestSkip()
    {
        skip = true;
    }

    private void Awake()
    {
        textBody = GetComponent<Text>();
    }

    public void SetNewText(string text)
    {
        textBody.text = text;
    }

    public void SetNewText(string text, float speed)
    {
        StartCoroutine(TextFadeIn(text, 1.0f / speed, 5, 0.1f / 10));
    }

    IEnumerator TextFadeIn(string text, float waitTime, int gAmount, float gWaitTime)
    {
        moving = true;

        char[] textChar = text.ToCharArray();
        Debug.Log(textChar.Length);
        int[] effect = new int[textChar.Length];
        int current = 0;
        float timer = 0;
        float timer2 = 0;
        for (int i = 0; i < effect.Length; i++)
        {
            if (char.IsControl(textChar[i]))
            {
                effect[i] = -1;
            }
            else
            {
                effect[i] = gAmount;
            }
        }
        while (effect[effect.Length - 1] != 0)
        {
            if (skip)
            {
                skip = false;
                moving = false;
                textBody.text = text;
                yield break;
            }
            timer += Time.deltaTime;
            if (timer > waitTime)
            {
                timer -= waitTime;
                current++;
                current = Mathf.Min(effect.Length - 1, current);
                
            }

            timer2 += Time.deltaTime;
            if (timer2 > gWaitTime)
            {
                while (timer2 > gWaitTime)
                {
                    timer2 -= gWaitTime;
                    for (int i = 0; i <= current; i++)
                    {
                        effect[i]--;
                        effect[i] = Mathf.Max(0, effect[i]);
                    }
                }
            }
            


            char[] showChar = new char[current + 1];
            for (int i = 0; i <= current; i++)
            {
                showChar[i] = (char)((int)textChar[i] + effect[i]);
            }

            textBody.text = new string(showChar);

            yield return null;
        }
        Debug.Log("effect,end");
        moving = false;
        skip = false;
    }
}
