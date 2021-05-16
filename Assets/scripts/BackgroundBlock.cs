using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBlock : BaseBlock
{


    public void SetColorMaterial(Color tileColor)
    {
        Material material = gameObject.GetComponent<MeshRenderer>().material;
        material.color = tileColor;
    }
}