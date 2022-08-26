using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU
{
    public class Intiator : MonoBehaviour
    {
        private void Awake()
        {
            SceneLoader.Instance.LoadScene("Gameplay");
        }
    }
}
