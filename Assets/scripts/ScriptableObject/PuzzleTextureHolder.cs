using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Levels Texture",menuName = "Craete new Texture Data List")]
public class PuzzleTextureHolder : ScriptableObject
{
    
    [SerializeField] public List<LevelTextureData> TextureDataList = new List<LevelTextureData>();

    public LevelTextureData GetLevelData(int levelDataIndex)
    {
        if (levelDataIndex < TextureDataList.Count)
        {
            return TextureDataList[levelDataIndex];
        }
        else
        {
            return null;
        }
        
    }
}

[System.Serializable]
public class LevelTextureData
{
    public Texture2D image;
    public int SlicePerline = 3;
    public int TimeToShuffle = 3;

    public LevelTextureData(Texture2D _image = null,int _slicePerline = 3,int _timeToShuffle = 3)
    {
        image = _image;
        SlicePerline = _slicePerline;
        TimeToShuffle = _timeToShuffle;
    }
}
