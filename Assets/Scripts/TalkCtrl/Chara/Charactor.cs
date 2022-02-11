using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charactor : MonoBehaviour
{
    [SerializeField]
    Sprite[] poses;

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void ChangePose(int code)
    {
        image.sprite = poses[code];
    }
}
