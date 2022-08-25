using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Agate.MVC.Core;

namespace TankU.PubSub
{
    public class PubSubContainer { }

    #region Message Module GameStatus

    #region GameStatus as Publisher
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
    public struct MessageSpawnBullet
    {
        public Transform shooter;
        public Transform bulletOutPos;

        public MessageSpawnBullet(Transform shooter, Transform bulletOutPos)
        {
            this.shooter = shooter;
            this.bulletOutPos = bulletOutPos;
        }
    }
    public struct MessageSpawnBomb
    {
        public Transform shooter;

        public MessageSpawnBomb(Transform shooter)
        {
            this.shooter = shooter;
        }
    }
    public struct Hit
    {
        int _hitDamage;

        public Hit(int damagePoint)
        {
            this._hitDamage = damagePoint;
        }
    }

    #region Message Module PowerUpSpawner

    public struct Bounce { }
    public struct Heal { }
    #endregion
}

