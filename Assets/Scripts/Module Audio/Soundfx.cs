using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace TankU.Audio
{
    [System.Serializable]
    public class Soundfx
    {
        public string name;
        public AudioClip clip;

        [HideInInspector]
        public AudioSource source;
    }
}


