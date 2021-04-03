using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState gameState;

    [SerializeField] private Puzzle _puzzle;
    [SerializeField] private PuzzleTextureHolder _puzzleTextureHolder;
    [SerializeField] private int currentPuzzleIndex = 0;

    public static int CurrentPuzzleSlicePerline;
    
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    private void Start()
    {
        LoadLevel(currentPuzzleIndex);
    }

    
    void LoadLevel(int currentPuzzleIndex)
    {
        LevelTextureData levelData = _puzzleTextureHolder.GetLevelData(currentPuzzleIndex);

        if (levelData != null)
        {
            CurrentPuzzleSlicePerline = levelData.SlicePerline;
            _puzzle.CreatePuzzle(levelData);
            
        }
        else
        {
            print("Data Not Found");
        }
    }

    public bool IsPuzzleCorrect()
    {
        if (_puzzle.IsAllBlockPuzzleCorrect())
        {
            print("Complete Puzzle");
            LoadNextPuzzle();
            return true;
        }

        return false;
    }


    void LoadNextPuzzle()
    {
        currentPuzzleIndex++;
        LoadLevel(currentPuzzleIndex);
    }
}

public enum GameState
{
    Pause,
    Play
}