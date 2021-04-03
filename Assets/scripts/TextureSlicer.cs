using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureSlicer
{
    /// <summary>
    /// Slice texture to block Puzzle
    /// </summary>
    /// <param name="image"></param>
    /// <param name="sliceSize"></param>
    /// <returns></returns>
    public static Texture2D[,] GetTextureSlice(Texture2D image, int sliceSize)
    {
        if (image == null) return null;


        int imageSize = Mathf.Min(image.height, image.width);
        int blockSize = imageSize / sliceSize;

        Texture2D[,] textureSlices = new Texture2D[sliceSize, sliceSize];
        for (int y = 0; y < sliceSize; y++)
        {
            for (int x = 0; x < sliceSize; x++)
            {
                Texture2D newSlice = new Texture2D(blockSize, blockSize);
                newSlice.SetPixels(image.GetPixels(blockSize * x, blockSize * y, blockSize, blockSize));
                newSlice.Apply();

                textureSlices[x, y] = newSlice;
            }
        }

        return textureSlices;
    }
}