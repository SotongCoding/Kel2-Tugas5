using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using TankU.GameRecord;

namespace TankU.MatchHistory
{
    public class MatchHistory : MonoBehaviour
    {
        [SerializeField]
        public TextMeshProUGUI BattleHistory;
        [SerializeField]
        private Transform MatchContainer;
        [SerializeField]
        private TextMeshProUGUI[] WinCount;
        // Start is called before the first frame update
        void Start()
        {
            foreach (var item in GameRecord.GameRecord.Instance.savedMatchData)
            {
                var MatchText = Instantiate(BattleHistory, MatchContainer);
                string LoserPlayer = "";
                Array.ForEach(item.losePlayers, (s) => LoserPlayer += s);
                MatchText.text = "Winner: P" + item.winPlayer + "| Loser: P" + LoserPlayer;
                Debug.Log("Capacity: " + GameRecord.GameRecord.Instance.savedMatchData.Capacity + "Count: " + GameRecord.GameRecord.Instance.savedMatchData.Count);
            }

            for (int i = 0; i < WinCount.Length; i++)
            {
                var matchData = new PlayerMatchRecord(i + 1);
                WinCount[i].text = "P" + (i + 1) + ": " + matchData.win;
            }
            //lambda Expression Half Life :) "(s) => losePlayer += s"
            //void SetString(int s)
            //{
            //    losePlayer += s;
            //}
        }
    }
}

