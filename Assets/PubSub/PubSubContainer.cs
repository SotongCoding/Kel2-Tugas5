using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Agate.MVC.Core;

namespace TankU.PubSub
{
    public class PubSubContainer { }

    #region Message Module GameStatus

    #region GameStatus as Publisher
    public struct MessageTimer
    {
        public float timer;

        public MessageTimer(float timer)
        {
            this.timer = timer;
        }
    }

    public struct MessageStartGameplay { }
    public struct MessageEndGameplay { }
    public struct MessageTieBreaker { }
    #endregion

    #region TimerGameplay as Publisher
    public struct MessageTimesUp { }
    #endregion

    #endregion


    public struct MessageUnitDie
    {
        public int id;
        public MessageUnitDie(int id)
        {
            this.id = id;
        }
    }

    public struct MessageGameoverUI
    {
        public string message;
        public MessageGameoverUI(string message)
        {
            this.message = message;
        }
    }
}


