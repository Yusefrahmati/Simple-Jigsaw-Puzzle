using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleTestProject
{
    public static class PuzzleUtility
    {
        public static float PuzzleWidth = 5f;

        /// <summary>
        /// Add movment Component to Gameobject
        /// </summary>
        /// <param name="target">Target Gameobject</param>
        /// <param name="targetPosition">Target End Position</param>
        /// <param name="speed">Speed movment to end position</param>
        public static void Move(this GameObject target, Vector2 targetPosition, float speed = 1)
        {
            MoveToward moveToward = target.AddComponent<MoveToward>();
            moveToward.targePosition = targetPosition;
            moveToward.speed = speed;
        }

        
        /// <summary>
        /// Divide the width of the puzzle
        /// </summary>
        /// <param name="slicePerLine"></param>
        /// <returns></returns>
        public static float GetBlockObjectScaleSize(int slicePerLine)
        {
            return PuzzleWidth / slicePerLine;
        }
    }
}