using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using TankU.PubSub;

using Agate.MVC.Core;
using TankU.GameSetting;

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
        private GameSetting.GameSetting _gameSetting;

        private Dictionary<string, AudioSource> playedSound = new Dictionary<string, AudioSource>();

        private void Start()
        {
            _gameSetting = GameSetting.GameSetting.Instance;
        }
        private void Awake()
        {
            sourceSoundfx = gameObject.AddComponent<AudioSource>();
            Subscriber();
            sourceSoundBgm = gameObject.AddComponent<AudioSource>();
            SetupAudioSourceSoundfx();
            SetupAudioSourceSoundBgm();
        }
        private void OnDestroy()
        {
            UnSubsriber();
        }
        void SetupAudioSourceSoundfx()
        {
            sourceSoundfx.volume = _gameSetting.savedData["soundSFX"];
            sourceSoundfx.playOnAwake = false;
            sourceSoundfx.loop = false;
        }
        void SetupAudioSourceSoundBgm()
        {
            sourceSoundBgm.volume = _gameSetting.savedData["soundBGM"];
            sourceSoundBgm.playOnAwake = false;
            sourceSoundBgm.loop = false;
        }


        private void Subscriber()
        {
            PublishSubscribe.Instance.Subscribe<MessageSoundfx>(ReceiveMessageSoundfx);
            PublishSubscribe.Instance.Subscribe<MessageSoundBgm>(ReceiveMessageSoundBgm);
            PublishSubscribe.Instance.Subscribe<MessagePlaySoundOnce>(ReceiveMessagePlaySoundOnce);
            PublishSubscribe.Instance.Subscribe<MessagePauseSoundOnce>(ReceiveMessagePauseSoundOnce);
        }
        private void UnSubsriber()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageSoundfx>(ReceiveMessageSoundfx);
            PublishSubscribe.Instance.Unsubscribe<MessageSoundBgm>(ReceiveMessageSoundBgm);
            PublishSubscribe.Instance.Unsubscribe<MessagePlaySoundOnce>(ReceiveMessagePlaySoundOnce);
            PublishSubscribe.Instance.Unsubscribe<MessagePauseSoundOnce>(ReceiveMessagePauseSoundOnce);

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
            if (!playedSound.ContainsKey(message.name))
            {
                var source = gameObject.AddComponent<AudioSource>();
                source.volume = _gameSetting.savedData["soundSFX"];
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
                playedSound[message.name].Play();
            }
        }
        private void ReceiveMessagePauseSoundOnce(MessagePauseSoundOnce message)
        {
            playedSound[message.name].Stop();
        }
    }

}


