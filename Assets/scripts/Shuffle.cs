using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleTestProject;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shuffle : MonoBehaviour
{
    [SerializeField] private Bounds _shuffleArea;
    

    //Move a list of block puzzle to random position in shuffle area 
    public void ShffleAllBlocks(params PuzzleBlock[] blockTransform)
    {
        GameManager.Instance.gameState = GameState.Play;
        for (int i = 0; i < blockTransform.Length; i++)
        {
            MoveBlockToNewRandomPosition(blockTransform[i].transform);
        }
    }

    private void MoveBlockToNewRandomPosition(Transform blockTransform)
    {
        blockTransform.gameObject.Move(GetRandomPositionInShffleArea(), 6);
    }

    Vector2 GetRandomPositionInShffleArea()
    {
        bool IsRightSide = (Random.value > 0.5f);
        return new Vector2(
            IsRightSide
                ? Random.Range(_shuffleArea.min.x, _shuffleArea.max.x)
                : Random.Range(_shuffleArea.min.x, _shuffleArea.max.x) * -1,
            Random.Range(_shuffleArea.min.y, _shuffleArea.max.y));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_shuffleArea.center, _shuffleArea.size);
        Gizmos.DrawWireCube(new Vector2(-_shuffleArea.center.x, _shuffleArea.center.y), _shuffleArea.size);

       
    }
}