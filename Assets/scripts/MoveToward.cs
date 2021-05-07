using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToward : Singleton<MoveToward>
{
    
    public void Move(Transform objectToMove, Vector3 targetPos, float speed)
    {
        StartCoroutine(MoveFromTo(objectToMove,targetPos,speed));
    }

    IEnumerator MoveFromTo(Transform objectToMove, Vector3 targetPos, float speed)
    {
        Vector3 a = objectToMove.transform.position;

        float step = (speed / (a - targetPos).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            objectToMove.position = Vector3.Lerp(a, targetPos, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        objectToMove.position = targetPos;
    }
}
