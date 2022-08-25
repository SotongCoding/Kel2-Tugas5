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
        [SerializeField]
        private Text timerText;

        private bool timeActive;

        private void Awake()
        {
            Subscriber();
            timerText.text = timerGameplay.ToString();
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
                timerText.text = Mathf.Round(timerGameplay).ToString();
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
