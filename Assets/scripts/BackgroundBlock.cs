using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBlock : BaseBlock
{
    public  void SetColorMaterial(bool IsGray)
    {
        Material material = gameObject.GetComponent<MeshRenderer>().material;
        material.shader = Shader.Find("Sprites/Default");
        if (IsGray)
        {
            material.color = Color.gray;
        }
        else
        {
            material.color = Color.white;
        }
    }
}