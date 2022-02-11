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
        if (GUILayout.Button("現在のシナリオをjsonファイルに書き出し"))
        {
            SaveJson();
        }
        if (GUILayout.Button("jsonファイルからシナリオをロード"))
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

        var path = EditorUtility.OpenFilePanel("ロードするシナリオを選択", "", "json");
        if (string.IsNullOrEmpty(path)) return;

        string json = File.ReadAllText(path);
        me.TalkOrders = JsonUtility.FromJson<TalkOrderList>(json).order;
    }
}
