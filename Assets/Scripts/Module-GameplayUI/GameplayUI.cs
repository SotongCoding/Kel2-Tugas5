using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agate.MVC.Core;
using TankU.GameStatus;
using TankU.Unit;
using TMPro;
using UnityEngine.UI;
using TankU.PubSub;
namespace TankU.GameplayUI
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI TimerTxT, TieBreakTxT;
        private GameStatus.TimerGameplay _TimerGameplay;
        [SerializeField]
        private GameObject[] Player;
        [SerializeField]
        private int[] HP;
        [SerializeField]
        private GameObject[] BombP1, BombP2, BombP3, BombP4;
        [SerializeField]
        private int[] BombLeft;
        [SerializeField]
        private Slider[] HealthBar;
        // Start is called before the first frame update
        void Start()
        {
            _TimerGameplay = GameObject.Find("GameStatus").GetComponent<TimerGameplay>();
            Player = new GameObject[2];
            HP = new int[Player.Length];
            Debug.Log("player length: " + Player.Length);
            for (int i = 0; i < HP.Length; i++)
            {
                HP[i] = GameObject.Find("P" + (i + 1)).GetComponent<Unit.Unit>()._health;
            }

            HealthBar = new Slider[Player.Length];
            for (int i = 0; i < HealthBar.Length; i++)
            {
                HealthBar[i] = GameObject.Find("P" + (i+1) + "-Hp").GetComponent<Slider>();
                HealthBar[i].maxValue = HP[i];
            }

            BombLeft = new int[Player.Length];
            for (int i = 0; i < BombLeft.Length; i++)
            {
                BombLeft[i] = 5;
            }
        }

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageSpawnBomb>(ReduceBomb);
            PublishSubscribe.Instance.Subscribe<MessageTieBreaker>(Tiebreaker);
        }
        // Update is called once per frame
        void Update()
        {
            TimerTxT.text = _TimerGameplay.timer.ToString();
            for (int i = 0; i < HealthBar.Length; i++)
            {
                HealthBar[i] = GameObject.Find("P" + (i+1) + "-Hp").GetComponent<Slider>();
                HealthBar[i].value = GameObject.Find("P" + (i + 1)).GetComponent<Unit.Unit>()._health;
            }

        }
        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageSpawnBomb>(ReduceBomb);
            PublishSubscribe.Instance.Unsubscribe<MessageTieBreaker>(Tiebreaker);
        }
        void ReduceBomb(MessageSpawnBomb message)
        {
            if (message.PlayerId == 1)
            {
                BombLeft[0]--;
                BombP1[BombLeft[0]].SetActive(false);
            }
            else if (message.PlayerId == 2)
            {
                BombLeft[1]--;
                BombP2[BombLeft[1]].SetActive(false);
            }
            else if (message.PlayerId == 3)
            {
                BombLeft[2]--;
                BombP3[BombLeft[2]].SetActive(false);
            }
            else if (message.PlayerId == 4)
            {
                BombLeft[3]--;
                BombP4[BombLeft[3]].SetActive(false);
            }
        }

        void Tiebreaker(MessageTieBreaker message)
        {
            TieBreakTxT.gameObject.SetActive(true);
        }
        
    }
}

