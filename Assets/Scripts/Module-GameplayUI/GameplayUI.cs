using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agate.MVC.Core;
using TankU.GameStatus;
using TankU.Unit;
using TMPro;
using UnityEngine.UI;
using TankU.PubSub;
using TankU.GameRecord;

namespace TankU.GameplayUI
{
    public class GameplayUI : MonoBehaviour
    {
        public int PlayerReady;
        [SerializeField]
        private TextMeshProUGUI TimerTxT, TieBreakTxT, playerWinText;
        [SerializeField]
        private TextMeshProUGUI[] PlayerWinTxT;
        private GameStatus.TimerGameplay _TimerGameplay;
        [SerializeField]
        private UnitStatusUI[] PlayerUI;
        [SerializeField] private GameObject gameOverPanel;

        void Start()
        {
            _TimerGameplay = GameObject.Find("GameStatus").GetComponent<TimerGameplay>();
            PublishSubscribe.Instance.Publish<MessageSoundBgm>(new MessageSoundBgm("gameplay"));
            ShowMatchData();
        }

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageSpawnBomb>(ReduceBomb);
            PublishSubscribe.Instance.Subscribe<MessageTieBreaker>(Tiebreaker);
            PublishSubscribe.Instance.Subscribe<MessageGameoverUI>(ShowGameOver);
        }
        // Update is called once per frame
        void Update()
        {
            TimerTxT.text = _TimerGameplay.timer.ToString();

        }
        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageSpawnBomb>(ReduceBomb);
            PublishSubscribe.Instance.Unsubscribe<MessageTieBreaker>(Tiebreaker);
            PublishSubscribe.Instance.Unsubscribe<MessageGameoverUI>(ShowGameOver);
        }
        void ReduceBomb(MessageSpawnBomb message)
        {
            PlayerUI[message.PlayerId - 1].ReduceBomb();
        }

        public void UpdateHealth(int id)
        {
            PlayerUI[id - 1].UpdateHealth();
        }

        void Tiebreaker(MessageTieBreaker message)
        {
            TieBreakTxT.gameObject.SetActive(true);
            PlayerUI[0].UpdateHealth();
            PlayerUI[1].UpdateHealth();
        }

        void ShowGameOver(MessageGameoverUI message)
        {
            playerWinText.text = message.playerWonText;
            gameOverPanel.SetActive(true);
        }

        public void SetPlayerColor()
        {
            foreach (var player_ui in PlayerUI)
            {
                player_ui.SendColor();
            }

        }
        public void StartGame()
        {
            PublishSubscribe.Instance.Publish<MessageSoundfx>(new MessageSoundfx("button_click"));
            PublishSubscribe.Instance.Publish<MessageStartGameplay>(new MessageStartGameplay());
        }

        public void Retry()
        {
            PublishSubscribe.Instance.Publish<MessageSoundfx>(new MessageSoundfx("button_click"));
            SceneLoader.Instance.LoadScene("Gameplay");
        }

        public void MainMenu()
        {
            PublishSubscribe.Instance.Publish<MessageSoundfx>(new MessageSoundfx("button_click"));
            SceneLoader.Instance.LoadScene("MainMenu");
        }

        public void ShowMatchData()
        {
            Debug.Log(PlayerWinTxT.Length);
            for (int i = 0; i < PlayerWinTxT.Length; i++)
            {
                var playerMatchData = new PlayerMatchRecord(i + 1);
                PlayerWinTxT[i].text = "P" + (i + 1) + ": " + playerMatchData.win;
            }

        }
    }
}

