using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem
{
    int poolLimit;
    Queue<IPoolObject> storedList = new Queue<IPoolObject>();
    Queue<IPoolObject> spawnedList = new Queue<IPoolObject>();

    public PoolingSystem(int poolLimit = 10)
    {
        this.poolLimit = poolLimit;
    }

    public IPoolObject CreateObject(IPoolObject objectPrefab, Vector3 spawnPos, Transform parent = null)
    {
        IPoolObject outObject;
        if (storedList.Count < 1)
        {
            outObject = MonoBehaviour.Instantiate(objectPrefab.gameObject).
            GetComponent<IPoolObject>();
            outObject.Initial(this);
        }
        else
        {
            outObject = storedList.Dequeue();
        }
        outObject.transform.position = spawnPos;
        outObject.transform.parent = parent;


        outObject.OnCreate();
        outObject.gameObject.SetActive(true);

        spawnedList.Enqueue(outObject);


        return outObject;

    }

    public void Store(IPoolObject poolObject)
    {
        storedList.Enqueue((IPoolObject)poolObject);
    }
}