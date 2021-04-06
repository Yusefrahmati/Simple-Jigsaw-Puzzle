using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleTestProject;
using UnityEngine;

public class PuzzleBlock : BaseBlock, IDragable
{
    public bool IsSolved;

    [SerializeField] private Vector2 _startPosition;
    private Vector2 _mouseOffset;
    private const float MAX_SCALE_SIZE = 1.1f;


    public override void Init(Texture2D image, Vector2 position)
    {
        _startPosition = transform.position = position;
        SetMainTextureMaterial(ref image);
    }

    //Set texture to main texture material and set shader to Unlit/Texture
    protected override void SetMainTextureMaterial(ref Texture2D image)
    {
        GetComponent<MeshRenderer>().material.shader = Shader.Find("Unlit/Texture");
        GetComponent<MeshRenderer>().material.mainTexture = image;
    }


    public void OnPointerClick(DragData dragData)
    {
        if (IsSolved) return;

        _mouseOffset = dragData.MouseOffset;

        HighlightBlockObject();
    }

    public void OnDrag(DragData dragData)
    {
        if (IsSolved) return;

        Vector2 currentPosition = (dragData.MousePosition - (Vector2) transform.position) - _mouseOffset;

        transform.Translate(currentPosition);
    }


    public void OnEndDrag()
    {
        if (IsSolved) return;

        if (IsCorrectPosition())
        {
            SetToSolvedBlock();
        }

        UnHighlightBlockObject();
    }

    void SetToSolvedBlock()
    {
        IsSolved = true;
        GameManager.Instance.IsPuzzleCorrect();
        transform.position = _startPosition;
        transform.position += Vector3.forward * 2;
    }

    bool IsCorrectPosition()
    {
        float halfBlockWidth = PuzzleUtility.GetBlockObjectScaleSize(Puzzle.currentPuzzlePerline) / 2;

        if (Vector2.Distance(_startPosition, transform.position) < halfBlockWidth)
        {
            return true;
        }

        return false;
    }


    private void HighlightBlockObject()
    {
        transform.localScale *= (Vector2.one * MAX_SCALE_SIZE);
        transform.position -= Vector3.forward * 2f;
    }


    private void UnHighlightBlockObject()
    {
        transform.localScale *= (Vector2.one / MAX_SCALE_SIZE);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}