using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrapObject : MonoBehaviour
{
    [SerializeField] private bool _isMouseDrag;

    private IDragable _dragableObjectTraget;
    private float _maximumRaycastDistance = 10;

    private void Update()
    {
        LeftMouseClickDown();

        LeftMouseButtonRealize();

        DragObject();
    }

    private void LeftMouseClickDown()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.gameState == GameState.Play)
        {
            RaycastHit hit;
            _dragableObjectTraget = GetDragableObject(out hit);
            if (_dragableObjectTraget != null)
            {
                _dragableObjectTraget?.OnPointerClick(GetDragDataFromMousePosition(_dragableObjectTraget));
                _isMouseDrag = true;
            }
        }
    }

    /// <summary>
    /// Return Object with drag feature
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    private IDragable GetDragableObject(out RaycastHit hit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction * _maximumRaycastDistance, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point);
            return _dragableObjectTraget = hit.collider.gameObject.GetComponent<IDragable>();
        }
        else
        {
            return _dragableObjectTraget = null;
        }
    }

    private void LeftMouseButtonRealize()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _dragableObjectTraget?.OnEndDrag();
            _isMouseDrag = false;
        }
    }

    private void DragObject()
    {
        if (_isMouseDrag && Input.GetMouseButton(0))
        {
            _dragableObjectTraget?.OnDrag(GetDragDataFromMousePosition(_dragableObjectTraget));
        }
    }


    public DragData GetDragDataFromMousePosition(IDragable dragedObject)
    {
        Transform objectTransform = ((PuzzleBlock) dragedObject).transform;
        
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseOffset = mousePosition - (Vector2)objectTransform.position;
        DragData dragData = new DragData(mousePosition, mouseOffset);
        return dragData;
    }
}

public class DragData
{
    public Vector2 MousePosition;
    public Vector2 MouseOffset;

    
    private Vector2 _delta;
    public DragData(Vector2 mousePosition, Vector2 mouseOffset)
    {
        MousePosition = mousePosition;
        MouseOffset = mouseOffset;
    }
    
}