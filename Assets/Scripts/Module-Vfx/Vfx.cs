using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.Vfx
{
    [System.Serializable]
    public class Vfx
    {
        public VfxObject visualPref;
        PoolingSystem.PoolingSystem pool = new PoolingSystem.PoolingSystem();
        public GameObject CreateObject(Vector3 pos)
        {
            return pool.CreateObject(visualPref, pos).gameObject;
        }
    }
}
