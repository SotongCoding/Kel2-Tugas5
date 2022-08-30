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
        

        public string playerWon { get; private set; }

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
        }

        private void DeterminePlayerWon()
        {
            playerWon = "Player " + unitOnCombat[0].unitId + " Won";
            //load total player dengan unitId menang
            //masukan ke variable dan tambahkan 1
            //save
        }    

        private void Subscriber()
        {
            PublishSubscribe.Instance.Subscribe<MessageStartGameplay>(ReceiveMessageStartGameplay);
            PublishSubscribe.Instance.Subscribe<MessageTimesUp>(ReceiveMessageTimesUp);
            PublishSubscribe.Instance.Subscribe<MessageUnitDie>(ReceiveMessageUnitDie);
        }
        private void UnSubsriber()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageStartGameplay>(ReceiveMessageStartGameplay);
            PublishSubscribe.Instance.Unsubscribe<MessageTimesUp>(ReceiveMessageTimesUp);
            PublishSubscribe.Instance.Unsubscribe<MessageUnitDie>(ReceiveMessageUnitDie);

        }

        #region Send Message
        private void StartGameplay() { PublishSubscribe.Instance.Publish<MessageStartGameplayTime>(new MessageStartGameplayTime()); }
        private void EndGameplay() { PublishSubscribe.Instance.Publish<MessageEndGameplayTime>(new MessageEndGameplayTime()); }
        private void TieBreaker() { PublishSubscribe.Instance.Publish<MessageTieBreaker>(new MessageTieBreaker()); }
        private void GameoverUI(string playerWon)
        {
            PublishSubscribe.Instance.Publish<MessageGameoverUI>(new MessageGameoverUI(playerWon));
        }
        #endregion

        #region Message Received
        private void ReceiveMessageStartGameplay(MessageStartGameplay message) { StartGameplay(); }
        private void ReceiveMessageTimesUp(MessageTimesUp message) { TieBreaker(); }
        private void ReceiveMessageUnitDie(MessageUnitDie message)
        {
            unitOnCombat.Remove(message.unit);
            if (unitOnCombat.Count == 1)
            {
                DeterminePlayerWon();
                GameoverUI(playerWon);
                EndGameplay();
                gameOverPanel.SetActive(true);
            }
        }
        #endregion
    }
}



