using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour, IPoolObject
{
    public PoolingSystem poolingSystem { private set; get; }
    void IPoolObject.Initial(PoolingSystem poolSystem)
    {
        Debug.Log("Intial" + poolSystem);
        poolingSystem = poolSystem;
    }
    public abstract void OnCreate();
    public virtual void StoreToPool()
    {
        Debug.Log(poolingSystem);
        poolingSystem.Store(this);
        gameObject.SetActive(false);
    }
}