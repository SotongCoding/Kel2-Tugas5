using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankU.GameRecord
{
    // For debugging GameSetting in Weapon scene

    public class TestAudio : MonoBehaviour
    {
        private GameRecord _gameSetting;
        AudioData _save = new();
        [SerializeField] private AudioSource _bgm;
        [SerializeField] private AudioSource _sfx;
        [SerializeField] private Toggle _bgmToggle;
        [SerializeField] private Toggle _sfxToggle;
        [SerializeField] private Slider _bgmSlider;
        [SerializeField] private Slider _sfxSlider;

        void Start()
        {
            _gameSetting = GameRecord.Instance;
            LoadAudio();
        }

        public void OnBGMVolumeChange(float value)
        {
            _bgm.volume = value;
            _save.soundBGM = _bgm.volume;
            _gameSetting.ConvertAudioToJSON(_save);
        }

        public void OnSFXVolumeChange(float value)
        {
            _sfx.volume = value;
            _save.soundSFX = _sfx.volume;
            _gameSetting.ConvertAudioToJSON(_save);
        }

        public void LoadAudio()
        {
            //_bgm.volume = _gameSetting.savedData["soundBGM"];
            _bgm.volume = _gameSetting.savedAudioData["soundBGM"];
            _bgmSlider.value = _bgm.volume;
            //_sfx.volume = _gameSetting.savedData["soundSFX"];
            _sfx.volume = _gameSetting.savedAudioData["soundSFX"];
            _sfxSlider.value = _sfx.volume;
        }
    }
}