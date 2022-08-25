using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace TankU.Audio
{
    [System.Serializable]
    public class SoundBgm
    {
        public string name;
        public AudioClip clip;

        [HideInInspector]
        public AudioSource source;
    }
}


