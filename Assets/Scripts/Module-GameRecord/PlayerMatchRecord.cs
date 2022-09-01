using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using TankU.GameRecord;

public struct PlayerMatchRecord
{
    public int win;
    public int lose;

    public PlayerMatchRecord(int playerId)
    {
        var matchList = GameRecord.Instance.savedMatchData;
        int countWin = 0;
        int countLose = 0;
        foreach (var item in matchList)
        {
            if (item.winPlayer == playerId) countWin++;
            if (item.losePlayers.ToList().Contains(playerId)) countLose++;
        }
        win = countWin;
        lose = countLose;
    }
}
