using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Agate.MVC.Core;

using TankU.PubSub;

namespace TankU.GameStatus
{
    public class GameStatus : MonoBehaviour
    {

        [SerializeField]
        private GameObject gameOverPanel;
        [SerializeField]
        private TextMeshProUGUI PlayerWinner;
        List<Unit.Unit> unitOnCombat = new List<Unit.Unit>();

        private string playerWon;

        void FindUnitReference()
        {
            unitOnCombat = new List<Unit.Unit>(FindObjectsOfType<Unit.Unit>());

        }
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
            FindUnitReference();
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
            unitOnCombat.Remove(message.unit);
            if (unitOnCombat.Count == 1)
            {
                playerWon = unitOnCombat[0].name + "Won";
            }
            GameoverUI(playerWon);
            EndGameplay();
            gameOverPanel.SetActive(true);
            PlayerWinner.text = unitOnCombat[0].name;
        }
        #endregion
    }
}



