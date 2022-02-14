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
        //���g�p(�A�N�e�B�u�ɂȂ��Ă��Ȃ�)�̃{�^����{��
        SelectButton targetButton = buttons.FirstOrDefault(x => !x.gameObject.activeSelf);

        //���g�p�̃{�^�����Ȃ��ꍇ�͐V���ɐ���
        if (targetButton == null)
        {
            targetButton = Instantiate(selectButtonPrefab, this.transform);
            buttons.Add(targetButton);
        }

        //�{�^���������ꂽ���Ƃ�SelectButtonManager���g�����m�ł���悤�ɂ���
        targetButton.Init(buttonName, () => OnButtonPushed(onButtonPushed));
        targetButton.gameObject.SetActive(true);
    }

    void OnButtonPushed(System.Action onButtonPushed)
    {
        foreach (var item in buttons)
        {
            //�S�Ẵ{�^�����\���ɂ���
            item.gameObject.SetActive(false);
        }
        onButtonPushed?.Invoke();
    }
}
