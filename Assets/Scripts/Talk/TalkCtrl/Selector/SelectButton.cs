using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    [SerializeField]
    Text buttonText;
    [SerializeField]
    Button button;

    System.Action onPushed;
    private void Awake()
    {
        button.onClick.AddListener(OnButtonPushed);
    }

    public void Init(string buttonText, System.Action onPushedAction)
    {
        this.buttonText.text = buttonText;
        onPushed = null;
        onPushed += onPushedAction;
    }

    private void OnButtonPushed()
    {
        onPushed?.Invoke();
    }
}
