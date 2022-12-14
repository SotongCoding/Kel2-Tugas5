using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankU.GameplayUI
{
    public class UnitStatusUI : MonoBehaviour
    {
        [SerializeField]
        private Unit.Unit Player;
        [SerializeField]
        private GameObject[] Bomb;
        [SerializeField]
        private int BombLeft;
        [SerializeField]
        private Slider HealthBar;
        [SerializeField]
        ColourSelector Selector;

        private void Start()
        {
            // var playerMatchData = new PlayerMatchRecord(Player.unitId);
            Selector.SetColorBtn(GameRecord.GameRecord.Instance
            .savedPlayerMilestone.Find(x => x.playerId.Equals(Player.unitId))
            .milestoneReach);
            var matchRec = new GameRecord.PlayerMatchRecord(Player.unitId);
            Debug.Log(Player.unitId + " Record Win : " + matchRec.win + " Lose : " + matchRec.lose);
        }

        public void UpdateHealth()
        {
            HealthBar.value = Player._health;
        }
        public void ReduceBomb()
        {
            BombLeft--;
            Bomb[BombLeft].SetActive(false);
        }

        public void SendColor()
        {
            Player.SetUnitColor(Selector.MainColour, Selector.SubColour);
            Debug.Log("Main Color: " + Selector.MainColour + "Sub Color: " + Selector.SubColour);
        }
    }
}

