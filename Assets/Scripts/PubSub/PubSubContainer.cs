using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Agate.MVC.Core;

namespace TankU.PubSub
{
    public class PubSubContainer { }

    #region Message Module GameStatus

    #region GameStatus as Publisher
    public struct MessageStartGameplayTime { }
    public struct MessageEndGameplayTime { }
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
    public struct MessageBounceTimeUp
    {
        public int unitId;

        public MessageBounceTimeUp(int unitId)
        {
            this.unitId = unitId;
        }
    }
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
        public bool useBouncing;
        public int unitId;

        public MessageSpawnBullet(Transform shooter, Transform bulletOutPos, bool useBouncing, int unitId)
        {
            this.shooter = shooter;
            this.bulletOutPos = bulletOutPos;
            this.useBouncing = useBouncing;
            this.unitId = unitId;
        }
    }

    public struct MessageSpawnBomb
    {
        public Transform shooter;
        public int PlayerId;

        public MessageSpawnBomb(Transform shooter, int playerId)
        {
            this.shooter = shooter;
            PlayerId = playerId;
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

    public struct MessageStartGameplay { }
}


