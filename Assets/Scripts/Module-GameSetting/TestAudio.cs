using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.GameSetting
{
    public class TestAudio : MonoBehaviour
    {
        private GameSetting _gameSetting;
        [SerializeField] private AudioSource _bgm;
        [SerializeField] private AudioSource _sfx;

        // Start is called before the first frame update
        void Start()
        {
            _gameSetting = GameSetting.Instance;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnMuteBGM()
        {
            if (_bgm.mute)
            {
                _bgm.mute = false;
            }
            else
            {
                _bgm.mute = true;
            }
            
            _gameSetting.SaveData(_bgm.name, _bgm.mute);
        }
    }
}