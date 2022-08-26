using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Agate.MVC.Core;

using TankU.PubSub;

namespace TankU.GameStatus
{
    // for countdown time gameplay
    public class TimerGameplay : MonoBehaviour
    {
        [SerializeField]
        private float timerGameplay = 100f;
        private float timeCurrent;


        private bool timeActive;

        private void Awake()
        {
            Subscriber();
        }
        private void OnDestroy()
        {
            UnSubscriber();
        }
        private void Start()
        {
            timeCurrent = timerGameplay;
        }

        public void Update()
        {
            if (timeActive)
            {
                // ambil data timeCurrent untuk ditampilkan di gameplay UI nanti
                timerGameplay -= Time.deltaTime;
                timeCurrent = Mathf.Round(timerGameplay);
                if(timerGameplay <= 0)
                {
                    TimesUp();
                    timeActive = false;
                    Debug.Log("Times UP");
                }
            }
        }

        private void Subscriber()
        {
            PublishSubscribe.Instance.Subscribe<MessageStartGameplay>(ReceiveMessageStartGameplay);
            PublishSubscribe.Instance.Subscribe<MessageEndGameplay>(ReceiveMessageEndGameplay);
        }
        private void UnSubscriber()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageStartGameplay>(ReceiveMessageStartGameplay);
            PublishSubscribe.Instance.Unsubscribe<MessageEndGameplay>(ReceiveMessageEndGameplay);
        }

        
        #region Send Message
        private void TimesUp() { PublishSubscribe.Instance.Publish<MessageTimesUp>(new MessageTimesUp()); }
        #endregion

        #region Message Received
        private void ReceiveMessageStartGameplay(MessageStartGameplay message) { timeActive = true; }
        private void ReceiveMessageEndGameplay(MessageEndGameplay message) { timeActive = false; }
        #endregion
    }
}

