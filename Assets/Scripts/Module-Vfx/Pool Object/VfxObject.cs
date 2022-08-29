using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VfxObject : PoolObject
{
    public override void OnCreate()
    {
        Invoke("StoreToPool", 3f);
    }
    public override void StoreToPool()
    {
        base.StoreToPool();
    }

}
