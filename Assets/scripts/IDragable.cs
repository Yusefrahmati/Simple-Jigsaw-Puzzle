using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragable
{
    void OnPointerClick();

    void OnDrag(Vector2 currentPosition);
    void OnDrag();

    void OnEndDrag();
   
 
}