using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using PuzzleTestProject;
using UnityEngine;

public class PuzzleArea : MonoBehaviour
{
    [Range(0.5f, 5.0f)] [SerializeField] private float puzzleExtend;
    private Bounds _puzzleArea;

    private void Awake()
    {
        PuzzleUtility.PuzzleWidth = puzzleExtend * 2;
    }

    void OnValidate()
    {
        _puzzleArea.extents = new Vector2(puzzleExtend, puzzleExtend);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_puzzleArea.center, _puzzleArea.size);
    }
}