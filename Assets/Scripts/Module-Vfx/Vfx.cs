using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.Vfx
{
    [System.Serializable]
    public class Vfx
    {
        public VfxObject visualPref;
        PoolingSystem pool = new PoolingSystem();
        public void CreateObject(Vector3 pos)
        {
            pool.CreateObject(visualPref, pos);
        }
    }
}
