using System.Collections;
using System.Collections.Generic;
using PuzzleTestProject;
using UnityEngine;

public class PuzzleBackground : Puzzle
{
    private List<BackgroundBlock> _backGroundBlockObject = new List<BackgroundBlock>();

    private const float BETWEEN_BLOCK_OFFSET = 0.1f;

    //Create a Grid Ob BackGround object with Level Data
    protected override void CreateGrid(LevelTextureData levelData)
    {
        bool isGray = false;
        RemoveGrid<BackgroundBlock>(_backGroundBlockObject);
        for (int y = 0; y < levelData.SlicePerline; y++)
        {
            
            for (int x = 0; x < levelData.SlicePerline; x++)
            {
                GameObject blockObject =  _pool.GetObjectFromPool();
                blockObject.transform.parent = transform;
                float blockScaleSize = PuzzleUtility.GetBlockObjectScaleSize(levelData.SlicePerline);
                blockObject.transform.localScale *= blockScaleSize - (BETWEEN_BLOCK_OFFSET / blockScaleSize);

                
                if (y % 2 == 0)
                {
                    var gray = x % 2 != 0 ? isGray = true : isGray = false;
                }
                else
                {
                    var gray = x % 2 != 0 ? isGray = false : isGray = true;
                }

                InitNewBackGroundBlock(blockObject, levelData.SlicePerline, new Vector2(x, y), isGray);
            }
        }
    }


    void InitNewBackGroundBlock(GameObject blockObject, int slicePerline, Vector2 coordinate, bool isGray)
    {
        blockObject.transform.parent = transform;
        Vector3 blockNewPosition = CalculateBlockPosition(coordinate);
        blockObject.transform.position = blockNewPosition + Vector3.forward * 3;
        blockObject.GetComponent<BackgroundBlock>().SetColorMaterial(isGray);
        _backGroundBlockObject.Add(blockObject.GetComponent<BackgroundBlock>());
    }
}