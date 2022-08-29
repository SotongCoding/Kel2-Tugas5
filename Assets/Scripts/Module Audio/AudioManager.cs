using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using TankU.PubSub;

using Agate.MVC.Core;

namespace TankU.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private Soundfx[] soundfx;
        [SerializeField]
        private SoundBgm[] soundBgm;

        private AudioSource sourceSoundfx;
        private AudioSource sourceSoundBgm;
        private Dictionary<string, AudioSource> playedSound = new Dictionary<string, AudioSource>();

        private void Awake()
        {
            sourceSoundfx = gameObject.AddComponent<AudioSource>();
            Subscriber();
            sourceSoundBgm = gameObject.AddComponent<AudioSource>();
        }

        private void Start()
        {
            SetupAudioSourceSoundfx();
            SetupAudioSourceSoundBgm();
        }
        private void OnDestroy()
        {
            UnSubsriber();
        }
        void SetupAudioSourceSoundfx()
        {
            sourceSoundfx.volume = GameSetting.GameSetting.Instance.savedData["soundSFX"];
            sourceSoundfx.playOnAwake = false;
            sourceSoundfx.loop = false;
        }
        void SetupAudioSourceSoundBgm()
        {
            sourceSoundBgm.volume = GameSetting.GameSetting.Instance.savedData["soundBGM"];
            sourceSoundBgm.playOnAwake = false;
            sourceSoundBgm.loop = true;
        }


        private void Subscriber()
        {
            PublishSubscribe.Instance.Subscribe<MessageSoundfx>(ReceiveMessageSoundfx);
            PublishSubscribe.Instance.Subscribe<MessageSoundBgm>(ReceiveMessageSoundBgm);
            PublishSubscribe.Instance.Subscribe<MessagePlaySoundOnce>(ReceiveMessagePlaySoundOnce);
            PublishSubscribe.Instance.Subscribe<MessagePauseSoundOnce>(ReceiveMessagePauseSoundOnce);
            PublishSubscribe.Instance.Subscribe<MessageLoadVolume>(ReceiveMessageLoadVolume);
        }
        private void UnSubsriber()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageSoundfx>(ReceiveMessageSoundfx);
            PublishSubscribe.Instance.Unsubscribe<MessageSoundBgm>(ReceiveMessageSoundBgm);
            PublishSubscribe.Instance.Unsubscribe<MessagePlaySoundOnce>(ReceiveMessagePlaySoundOnce);
            PublishSubscribe.Instance.Unsubscribe<MessageLoadVolume>(ReceiveMessageLoadVolume);

        }

        // Message Received
        private void ReceiveMessageSoundfx(MessageSoundfx message)
        {
            Soundfx s = Array.Find(soundfx, sound => sound.name == message.name);
            if (s == null)
            {
                Debug.LogWarning("Sound : " + s + " not found");
                return;
            }
            sourceSoundfx.PlayOneShot(s.clip);
        }
        private void ReceiveMessageSoundBgm(MessageSoundBgm message)
        {
            SoundBgm s = Array.Find(soundBgm, sound => sound.name == message.name);
            if (s == null)
            {
                Debug.LogWarning("Sound : " + s + " not found");
                return;
            }
            sourceSoundBgm.clip = s.clip;
            sourceSoundBgm.Play();
        }
        private void ReceiveMessagePlaySoundOnce(MessagePlaySoundOnce message)
        {
            AudioSource source;
            if (!playedSound.ContainsKey(message.name))
            {
                source = gameObject.AddComponent<AudioSource>();
                source.volume = GameSetting.GameSetting.Instance.savedData["soundSFX"];
                Soundfx s = Array.Find(soundfx, sound => sound.name == message.name);
                if (s == null)
                {
                    Debug.LogWarning("Sound : " + s + " not found");
                    return;
                }
                playedSound.Add(message.name, source);
                source.clip = s.clip;
                source.Play();
            }
            else
            {
                if (playedSound[message.name].isPlaying) { return; }
                source = playedSound[message.name];
                playedSound[message.name].volume = GameSetting.GameSetting.Instance.savedData["soundSFX"];
                playedSound[message.name].Play();
            }
            source.volume = GameSetting.GameSetting.Instance.savedData["soundSFX"];
        }
        private void ReceiveMessagePauseSoundOnce(MessagePauseSoundOnce message)
        {
            playedSound[message.name].Stop();
        }
        private void ReceiveMessageLoadVolume(MessageLoadVolume message)
        {
            Debug.Log("Set Volume");
            sourceSoundfx.volume = GameSetting.GameSetting.Instance.savedData["soundSFX"];
            sourceSoundBgm.volume = GameSetting.GameSetting.Instance.savedData["soundBGM"];
        }
    }

}


