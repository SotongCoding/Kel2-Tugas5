using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.PoolingSystem
{
    public abstract class PoolObject : MonoBehaviour, IPoolObject
    {
        public PoolingSystem poolingSystem { private set; get; }
        void IPoolObject.Initial(PoolingSystem poolSystem)
        {
            Debug.Log("Intial" + poolSystem);
            poolingSystem = poolSystem;
        }
        public abstract void OnCreate();
        public void StoreToPool()
        {
            Debug.Log(poolingSystem);
            poolingSystem.Store(this);
            gameObject.SetActive(false);
        }
    }
}