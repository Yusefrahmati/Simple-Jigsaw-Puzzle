using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBlock : MonoBehaviour
{
    public virtual void Init(Texture2D image, Vector2 position)
    {
        
    }

    protected virtual void SetMainTextureMaterial(ref Texture2D image)
    {
        
    }
}