using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    private Queue<GameObject> _blockPool = new Queue<GameObject>();

    private void Awake()
    {
        AddObjectToPool();
    }

    void AddObjectToPool()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject block = Instantiate(_prefab);

            block.transform.parent = transform;
            _blockPool.Enqueue(block);
            block.SetActive(false);
        }
    }

    public GameObject GetObjectFromPool()
    {
        if (_blockPool.Count <= 0)
        {
            AddObjectToPool();
        }

        GameObject block = _blockPool.Dequeue();
        block.SetActive(true);
        
        return block;
    }
}