using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

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
            //sourceSoundfx.volume = 
            sourceSoundfx.playOnAwake = false;
            sourceSoundfx.loop = false;
        }
        void SetupAudioSourceSoundBgm()
        {
            //sourceSoundBgm.volume = 
            sourceSoundBgm.playOnAwake = false;
            sourceSoundBgm.loop = false;
        }


        private void Subscriber()
        {
            PublishSubscribe.Instance.Subscribe<MessageSoundfx>(ReceiveMessageSoundfx);
            PublishSubscribe.Instance.Subscribe<MessageSoundBgm>(ReceiveMessageSoundBgm);
        }
        private void UnSubsriber()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageSoundfx>(ReceiveMessageSoundfx);
            PublishSubscribe.Instance.Unsubscribe<MessageSoundBgm>(ReceiveMessageSoundBgm);

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
            Soundfx s = Array.Find(soundfx, sound => sound.name == message.name);
            if (s == null)
            {
                Debug.LogWarning("Sound : " + s + " not found");
                return;
            }
            sourceSoundBgm.clip = s.clip;
            sourceSoundBgm.Play();
        }
    }

    public struct MessageSoundfx
    {
        public string name;
        public MessageSoundfx(string name) { this.name = name; }
            
    }
    public struct MessageSoundBgm
    {
        public string name;
        public MessageSoundBgm(string name) { this.name = name; }
            
    }
}


