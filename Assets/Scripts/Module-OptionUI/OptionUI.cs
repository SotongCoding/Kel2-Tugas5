using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TankU.GameSetting;

namespace TankU.OptionUI
{
    public class OptionUI : MonoBehaviour
    {
        [SerializeField]
        private Slider sfxVolumeSlider;
        [SerializeField]
        private Slider bgmVolumeSlider;
        private float sfxCurrently;
        private float bgmCurrently;

        [SerializeField]
        private Sprite iconMute, iconUnmute;
        [SerializeField]
        private Image[] iconButton;
        private bool[] isMute;

        private GameSetting.GameSetting _gameSetting;
        SaveData _save = new();

        private void Start()
        {
            _gameSetting = GameSetting.GameSetting.Instance;
            isMute = new bool[iconButton.Length];
            Load();
        }
        private void Update()
        {
            sfxVolumeSlider.value = sfxCurrently;
            bgmVolumeSlider.value = bgmCurrently;

            if (sfxCurrently != 0) { isMute[0] = false; }
            if (bgmCurrently != 0) { isMute[1] = false; }

            iconButton[0].sprite = isMute[0] ? iconMute : iconUnmute;
            iconButton[1].sprite = isMute[1] ? iconMute : iconUnmute;

        }

        public void SetVolumeSfx()
        {
            sfxCurrently = sfxVolumeSlider.value;
            Save();
        }
        public void SetVolumeBgm()
        {
            bgmCurrently = bgmVolumeSlider.value;
            Save();
        }
        public void SwtichButton(int id)
        {
            if (isMute[id])
            {
                isMute[id] = false;
                iconButton[id].sprite = iconUnmute;
                if(id == 0) { sfxCurrently = 1; }
                if(id == 1) { bgmCurrently = 1; }
                Save();
            }
            else
            {
                isMute[id] = true;
                iconButton[id].sprite = iconMute;
                if (id == 0) { sfxCurrently = 0; }
                if (id == 1) { bgmCurrently = 0; }
                Save();
            }
        }

        private void Save()
        {
            _save.soundBGM = bgmCurrently;
            _save.soundSFX = sfxCurrently;
            _gameSetting.ConvertToJSON(_save);
        }

        private void Load()
        {
            // load slider setting
            sfxCurrently = _gameSetting.savedData["soundSFX"];
            bgmCurrently = _gameSetting.savedData["soundBGM"];

            // load mute button setting
            if (sfxCurrently == 0)
            {
                isMute[0] = true;
                iconButton[0].sprite = iconMute;
            }
            if (bgmCurrently == 0)
            {
                isMute[1] = true;
                iconButton[1].sprite = iconMute;
            }
        }
    }
}