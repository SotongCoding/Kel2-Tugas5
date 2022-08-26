using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        private void Start()
        {
            isMute = new bool[iconButton.Length];
            Load();
        }
        private void Update()
        {
            sfxVolumeSlider.value = sfxCurrently;
            bgmVolumeSlider.value = bgmCurrently;

            Save("sfxVolume", sfxCurrently);
            Save("bgmVolume", bgmCurrently);

            if (sfxCurrently != 0) { isMute[0] = false; }
            if (bgmCurrently != 0) { isMute[1] = false; }

            iconButton[0].sprite = isMute[0] ? iconMute : iconUnmute;
            iconButton[1].sprite = isMute[1] ? iconMute : iconUnmute;

        }

        public void SetVolumeSfx()
        {
            sfxCurrently = sfxVolumeSlider.value;
        }
        public void SetVolumeBgm()
        {
            bgmCurrently = bgmVolumeSlider.value;
            //GameSetting.Instance.SaveData("sfxVolume", sfxCurrently);
        }
        public void SwtichButton(int id)
        {
            if (isMute[id])
            {
                isMute[id] = false;
                iconButton[id].sprite = iconUnmute;
                if(id == 0) { sfxCurrently = 1; }
                if(id == 1) { bgmCurrently = 1; }
            }
            else
            {
                isMute[id] = true;
                iconButton[id].sprite = iconMute;
                if (id == 0) { sfxCurrently = 0; }
                if (id == 1) { bgmCurrently = 0; }
            }
        }

        private void Save(string name, float value)
        {
            PlayerPrefs.SetFloat(name, value);
            Debug.Log(PlayerPrefs.GetFloat(name));
        }

        private void Load()
        {
            // load slider setting
            sfxCurrently = PlayerPrefs.GetFloat("sfxVolume");
            bgmCurrently = PlayerPrefs.GetFloat("bgmVolume");

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