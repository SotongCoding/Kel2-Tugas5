using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU
{
    public class Initiator : MonoBehaviour
    {
        
        private void Awake()
        {
            SceneLoader.Instance.LoadScene("Gameplay");
        }
    }
}