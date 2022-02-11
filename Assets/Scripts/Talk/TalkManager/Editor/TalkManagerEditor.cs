using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

[CustomEditor(typeof(TalkManager))]
public class TalkManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("���݂̃V�i���I��json�t�@�C���ɏ����o��"))
        {
            SaveJson();
        }
        if (GUILayout.Button("json�t�@�C������V�i���I�����[�h"))
        {
            ReadJson();
        }
        base.OnInspectorGUI();
    }

    

    void SaveJson()
    {
        TalkManager me = (TalkManager)target;
        if (me == null) return;

        var path = EditorUtility.SaveFilePanel("", "", "TalkData", "json");
        if (string.IsNullOrEmpty(path)) return;

        TalkOrderList data = new TalkOrderList();
        data.order = me.TalkOrders;

        string json = JsonUtility.ToJson(data);
        
        File.WriteAllText(path, json);
    }

    void ReadJson()
    {
        TalkManager me = (TalkManager)target;
        if (me == null) return;

        var path = EditorUtility.OpenFilePanel("���[�h����V�i���I��I��", "", "json");
        if (string.IsNullOrEmpty(path)) return;

        string json = File.ReadAllText(path);
        me.TalkOrders = JsonUtility.FromJson<TalkOrderList>(json).order;
    }
}
