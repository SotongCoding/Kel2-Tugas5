using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.Vfx
{
    public class VfxSelfDestruct : MonoBehaviour
    {
        [SerializeField]
        private float time = 3f;

        private void Start()
        {
            Invoke("SelfDestruct", time);
        }
        
        void SelfDestruct()
        {
            Destroy(gameObject);
        }
    }
}