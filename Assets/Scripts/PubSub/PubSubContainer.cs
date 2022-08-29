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

    #region Message Module Vfx
    public struct MessageVfx
    {
        public string name;
        public Vector3 position;
        public MessageVfx(string name, Vector3 position)
        {
            this.name = name;
            this.position = position;
        }
    }
    #endregion

    #region Message Module PowerUpSpawner

    public struct Bounce { }
    public struct Heal { }
    #endregion

    #region Module Audio
    public struct MessageSoundfx
    {
        public string name;
        public MessageSoundfx(string name) { this.name = name; }

    }
    public struct MessageSoundBgm
    {
        public string name;
        public MessageSoundBgm(string name) { this.name = name; }

    }
    public struct MessagePlaySoundOnce
    {
        public string name;
        public MessagePlaySoundOnce(string name) { this.name = name; }
    }
    public struct MessagePauseSoundOnce
    {
        public string name;
        public MessagePauseSoundOnce(string name) { this.name = name; }
    }
    public struct MessageLoadVolume { }
    #endregion

    public struct MessageUnitDie
    {
        public Unit.Unit unit;
        public MessageUnitDie(Unit.Unit unit)
        {
            this.unit = unit;
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

    public struct ColourIn
    {
        public string message;

        public ColourIn(string message)
        {
            this.message = message;
        }
    }

}


