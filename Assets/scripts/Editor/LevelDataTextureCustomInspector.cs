using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PuzzleTextureHolder))]
public class LevelDataTextureCustomInspector : Editor
{
    private SerializedProperty levelsData;

    private List<LevelTextureData> _levelTextureDatas;

    private void OnEnable()
    {
        levelsData = serializedObject.FindProperty("TextureDataList");
        _levelTextureDatas = ((PuzzleTextureHolder) target).TextureDataList;
    }

    public override void OnInspectorGUI()
    {
        
        
        DrawAddBtn();
        
        DrawLine(Color.green);
        
        foreach (LevelTextureData levelData in _levelTextureDatas.ToArray())
        {
            DrawDataLevelElement(levelData);
        }
    }

    void DrawDataLevelElement(LevelTextureData levelData)
    {
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();
        DrawRemoveBtn(levelData);
        levelData.image = DrawTextureField("Texture", ref levelData.image);
        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();
        GUILayout.Space(25);

        DrawIntField("slice Perline", ref levelData.SlicePerline);

        GUILayout.Space(15);

        DrawIntField("shuffle time", ref levelData.TimeToShuffle);

        GUILayout.Space(15);
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();


        DrawLine(Color.gray);
    }

    private Texture2D DrawTextureField(string name, ref Texture2D texture)
    {
        GUILayout.BeginVertical();

        var result = (Texture2D) EditorGUILayout.ObjectField(texture, typeof(Texture2D), false, GUILayout.Width(70),
            GUILayout.Height(70));
        DrawTextLabel(name, 70);
        GUILayout.EndVertical();

        return result;
    }

    private void DrawIntField(string name, ref int value)
    {
        int widthSize = 30;
        var style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.UpperCenter;


        GUILayout.BeginHorizontal();
        GUILayout.Label(name, style);
        if (GUILayout.Button("<<", GUILayout.Width(widthSize)))
            if (value - 1 >= 3)
                value--;
        value = EditorGUILayout.IntField(value, style, GUILayout.Width(widthSize));
        if (GUILayout.Button(">>", GUILayout.Width(widthSize)))
            if (value <= 10)
                value++;
        GUILayout.Space(150);
        GUILayout.EndHorizontal();
    }

    private void DrawRemoveBtn(LevelTextureData levelData)
    {
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("X", GUILayout.Width(30)))
        {
            _levelTextureDatas.Remove(levelData);
        }

        GUI.backgroundColor = Color.gray;
    }


    private void DrawAddBtn()
    {
        GUILayout.BeginVertical();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Add Level", GUILayout.Height(35)))
        {
            _levelTextureDatas.Add(new LevelTextureData());
        }

        GUILayout.EndVertical();
        GUI.backgroundColor = Color.gray;
    }

    private void DrawUpdateFromResourcesFolder()
    {

        if (GUILayout.Button("Update From Resources Folder",GUILayout.Height(35)))
        {
            Texture2D[] textureFromResourcesFolder = Resources.LoadAll<Texture2D>("");
            _levelTextureDatas = new List<LevelTextureData>();
        
            for (int i = 0; i < textureFromResourcesFolder.Length; i++)
            {
                _levelTextureDatas.Add(new LevelTextureData(textureFromResourcesFolder[i]));
            }   
        }

    }

    private void DrawTextLabel(string name, float width, TextAnchor textAnchor = TextAnchor.UpperCenter)
    {
        var style = new GUIStyle(GUI.skin.label);
        style.alignment = TextAnchor.UpperCenter;
        style.fixedWidth = width;
        GUILayout.Label(name, style);
    }

    private void DrawLine(Color color)
    {
        GUI.backgroundColor = color;
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUI.backgroundColor = Color.gray;
    }
}