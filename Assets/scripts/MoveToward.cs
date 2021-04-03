using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToward : MonoBehaviour
{

    public Vector2 targePosition;
    public float speed = 1;
    

    private float step;
    void Update()
    {
        step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targePosition,step);

        if (Vector3.Distance(transform.position,targePosition)<0.01f)
        {
            this.enabled = false;
        }
    }
}
