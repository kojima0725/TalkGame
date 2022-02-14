using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SelectButtonManager : MonoBehaviour
{
    [SerializeField]
    SelectButton selectButtonPrefab;

    List<SelectButton> buttons = new List<SelectButton>();

    public void SetNewButton(string buttonName, System.Action onButtonPushed)
    {
        //未使用(アクティブになっていない)のボタンを捜索
        SelectButton targetButton = buttons.FirstOrDefault(x => !x.gameObject.activeSelf);

        //未使用のボタンがない場合は新たに生成
        if (targetButton == null)
        {
            targetButton = Instantiate(selectButtonPrefab, this.transform);
            buttons.Add(targetButton);
        }

        //ボタンが押されたことをSelectButtonManager自身も検知できるようにする
        targetButton.Init(buttonName, () => OnButtonPushed(onButtonPushed));
        targetButton.gameObject.SetActive(true);
    }

    void OnButtonPushed(System.Action onButtonPushed)
    {
        foreach (var item in buttons)
        {
            //全てのボタンを非表示にする
            item.gameObject.SetActive(false);
        }
        onButtonPushed?.Invoke();
    }
}
