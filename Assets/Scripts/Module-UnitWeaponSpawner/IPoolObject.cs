using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolObject
{
    public Transform transform { get; }
    public GameObject gameObject { get; }
    PoolingSystem poolingSystem { get; }

    void Initial(PoolingSystem poolSystem);
    void OnCreate();
    void StoreToPool();
}
