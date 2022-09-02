using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Agate.MVC.Core;
using TankU.GameRecord;
using TankU.PubSub;

namespace TankU.GameStatus
{
    public class GameStatus : MonoBehaviour
    {
        List<Unit.Unit> unitOnCombat = new List<Unit.Unit>();
        List<int> loseUnits = new List<int>();

        public string playerWon { get; private set; }

        void FindUnitReference()
        {
            unitOnCombat = new List<Unit.Unit>(FindObjectsOfType<Unit.Unit>());
            //loseUnits = new List<Unit.Unit>(unitOnCombat);

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
            int winPlayerId = unitOnCombat[0].unitId;

            GameRecord.GameRecord.Instance.ConvertMatchHistoryToJSON(winPlayerId, loseUnits.ToArray());
            //Milestone Win
            var matchData = new GameRecord.PlayerMatchRecord(winPlayerId);
            GameRecord.GameRecord.Instance.CalculateMilestone(matchData);
            //MileStoneLose
            for (int i = 0; i < loseUnits.Count; i++)
            {
                var loseMatchData = new GameRecord.PlayerMatchRecord(loseUnits[i]);
                GameRecord.GameRecord.Instance.CalculateMilestone(loseMatchData);
            }

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
            loseUnits.Add(message.unit.unitId);

            if (unitOnCombat.Count == 1)
            {
                DeterminePlayerWon();
                GameoverUI(playerWon);
                EndGameplay();
                PublishSubscribe.Instance.Publish<MessageSoundfx>(new MessageSoundfx("gameover"));
            }
        }
       
        #endregion
    }
}



