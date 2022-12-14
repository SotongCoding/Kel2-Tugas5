using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Agate.MVC.Core;
using TankU.PubSub;


namespace TankU.Vfx
{
    public class VfxManager : MonoBehaviour
    {
        [SerializeField]
        private Vfx[] visualEffect;


        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageVfx>(ReceiveMessageVfx);
        }

        private void ReceiveMessageVfx(MessageVfx message)
        {
            Vfx v = Array.Find(visualEffect, vfx => vfx.visualPref.name == message.name);
            //Instantiate(v.visualPref, message.position, Quaternion.identity);
            v.CreateObject(message.position).transform.SetParent(this.transform);

        }
        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageVfx>(ReceiveMessageVfx);
        }
    }
}
