using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Agate.MVC.Core;

using TankU.PubSub;

namespace TankU.GameStatus
{
    public class GameStatus : MonoBehaviour
    {
        [SerializeField]
        private float timerGameplay = 120f;
        //[SerializeField]
        //private GameObject gameOverPanel;


        private string playerWon;

        private void Awake()
        {
            Subscriber();
        }
        private void OnDisable()
        {
            UnSubsriber();
        }

        private void Start()
        {
            SetTimerGameplay();
            StartGameplay();
        }

        private void Subscriber()
        {
            PublishSubscribe.Instance.Subscribe<MessageTimesUp>(ReceiveMessageTimesUp);
            PublishSubscribe.Instance.Subscribe<MessageUnitDie>(ReceiveMessageUnitDie);
        }
        private void UnSubsriber()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageTimesUp>(ReceiveMessageTimesUp);
            PublishSubscribe.Instance.Unsubscribe<MessageUnitDie>(ReceiveMessageUnitDie);

        }
        #region Send Message
        private void SetTimerGameplay() { PublishSubscribe.Instance.Publish<MessageTimer>(new MessageTimer(timerGameplay)); }
        private void StartGameplay() { PublishSubscribe.Instance.Publish<MessageStartGameplay>(new MessageStartGameplay()); }
        private void EndGameplay() { PublishSubscribe.Instance.Publish<MessageEndGameplay>(new MessageEndGameplay()); }
        private void TieBreaker() { PublishSubscribe.Instance.Publish<MessageTieBreaker>(new MessageTieBreaker()); }
        private void GameoverUI(string playerWon)
        {
            PublishSubscribe.Instance.Publish<MessageGameoverUI>(new MessageGameoverUI(playerWon));
        }
        #endregion

        #region Message Received
        private void ReceiveMessageTimesUp(MessageTimesUp message) { TieBreaker(); }
        private void ReceiveMessageUnitDie(MessageUnitDie message)
        {
            if (message.id == 1)
                playerWon = "Player 2 Win";
            else if (message.id == 2)
                playerWon = "Player 1 Win";

            GameoverUI(playerWon);
            EndGameplay();
            //gameOverPanel.SetActive(true);
        }
        #endregion
    }
}



