using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragable
{
    void OnPointerClick(DragData dragData);

    void OnDrag(DragData dragData);
    void OnEndDrag();
    
   
 
}