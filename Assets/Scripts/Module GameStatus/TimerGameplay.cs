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

        private bool timeActive = false;
        [HideInInspector]
        public float timer;

        private void Awake()
        {
            Subscriber();
        }
        private void OnDestroy()
        {
            UnSubscriber();
        }

        public void Update()
        {
            if (timeActive)
            {
                timerGameplay -= Time.deltaTime;
                timer = Mathf.Round(timerGameplay);
                if (timerGameplay <= 0)
                {
                    TimesUp();
                    timeActive = false;
                    Debug.Log("Times UP");
                }
            }
        }

        private void Subscriber()
        {
            PublishSubscribe.Instance.Subscribe<MessageStartGameplayTime>(ReceiveMessageStartGameplayTime);
            PublishSubscribe.Instance.Subscribe<MessageEndGameplayTime>(ReceiveMessageEndGameplayTime);
        }
        private void UnSubscriber()
        {
            
            PublishSubscribe.Instance.Unsubscribe<MessageStartGameplayTime>(ReceiveMessageStartGameplayTime);
            PublishSubscribe.Instance.Unsubscribe<MessageEndGameplayTime>(ReceiveMessageEndGameplayTime);
        }

        
        #region Send Message
        private void TimesUp() { PublishSubscribe.Instance.Publish<MessageTimesUp>(new MessageTimesUp()); }
        #endregion

        #region Message Received
        private void ReceiveMessageStartGameplayTime(MessageStartGameplayTime message) { timeActive = true; }
        private void ReceiveMessageEndGameplayTime(MessageEndGameplayTime message) { timeActive = false; }
        #endregion
    }
}

