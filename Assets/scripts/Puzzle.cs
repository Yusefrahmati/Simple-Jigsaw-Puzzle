using System;
using System.Collections;
using System.Collections.Generic;
using PuzzleTestProject;
using UnityEngine;
using UnityEngine.Serialization;

public class Puzzle : MonoBehaviour
{
    private int _timeToShufflePuzzle;
    private List<PuzzleBlock> allBlocksList = new List<PuzzleBlock>();
    private Shuffle _shuffle;
    private PuzzleBackground _puzzleBackground;

    [SerializeField] protected ObjectPool _pool;

    public static int currentPuzzlePerline;

    private void Awake()
    {
        _shuffle = GetComponent<Shuffle>();
        _puzzleBackground = GetComponentInChildren<PuzzleBackground>();
    }


    public void CreatePuzzle(LevelTextureData levelData)
    {
        _timeToShufflePuzzle = levelData.TimeToShuffle;
        _puzzleBackground.CreateGrid(levelData);
        currentPuzzlePerline = levelData.SlicePerline;
        RemoveGrid<PuzzleBlock>(allBlocksList);
        CreateGrid(levelData);
        StartCoroutine(Shuffle());
    }

    protected void RemoveGrid<T>(List<T> blockList) where T : MonoBehaviour
    {
        for (int i = 0; i < blockList.Count; i++)
        {
            blockList[i].gameObject.SetActive(false);
        }
    }

    //Create a grid of puzzle Block with level data
    protected virtual void CreateGrid(LevelTextureData levelData)
    {
        Texture2D[,] textureSlices = TextureSlicer.GetTextureSlice(levelData.image, levelData.SlicePerline);
        if (textureSlices == null)
        {
            Debug.LogWarning("Texture not found");
            return;
        }

        for (int y = 0; y < levelData.SlicePerline; y++)
        {
            for (int x = 0; x < levelData.SlicePerline; x++)
            {
                GameObject blockObject = _pool.GetObjectFromPool();
                SetBlockObjectParentAndScale(blockObject);
                Vector2 blockNewPosition = CalculateBlockPosition( new Vector2(x, y));
                InitNewBlockObject(ref blockObject, textureSlices[x, y], blockNewPosition);
            }
        }
    }

    private void SetBlockObjectParentAndScale(GameObject blockObject)
    {
        blockObject.transform.parent = transform;
        blockObject.transform.localScale *= PuzzleUtility.GetBlockObjectScaleSize(GameManager.CurrentPuzzleSlicePerline);
    }

    // Calculate new position from a 2d vector coordinate
    protected Vector2 CalculateBlockPosition( Vector2 coordinate)
    {
        int slicePerline = GameManager.CurrentPuzzleSlicePerline;
        
        float blockScaleSize = PuzzleUtility.GetBlockObjectScaleSize(slicePerline);
        return -Vector2.one * blockScaleSize * (slicePerline - 1) / 2 + coordinate * blockScaleSize;
    }

    // Create new puzzle block
    protected void InitNewBlockObject(ref GameObject blockObject, Texture2D textureSlices, Vector2 newPosition)
    {
        blockObject.AddComponent<PuzzleBlock>();
        blockObject.transform.parent = transform;
        blockObject.AddComponent<BoxCollider>();

        PuzzleBlock block = blockObject.GetComponent<PuzzleBlock>();
        block.Init(textureSlices, newPosition);
        allBlocksList.Add(block);
    }

    //Move all block puzzle to random position in shuffle area 
    IEnumerator Shuffle()
    {
        yield return new WaitForSeconds(_timeToShufflePuzzle);
        _shuffle.ShffleAllBlocks(allBlocksList.ToArray());
    }

    private List<PuzzleBlock> GetAllBlockList()
    {
        List<PuzzleBlock> AllBlocks = new List<PuzzleBlock>();
        foreach (Transform childTransform in transform)
        {
            AllBlocks.Add(childTransform.GetComponent<PuzzleBlock>());
        }

        print(AllBlocks.Count);
        return AllBlocks;
    }


    public bool IsAllBlockPuzzleCorrect()
    {
        for (int i = 0; i < allBlocksList.Count; i++)
        {
            if (!allBlocksList[i].IsSolved)
            {
                return false;
            }
        }

        return true;
    }
}

