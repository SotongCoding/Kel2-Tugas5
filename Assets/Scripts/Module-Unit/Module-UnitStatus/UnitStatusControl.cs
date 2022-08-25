using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankU.PubSub;
using Agate.MVC.Core;
using System;

namespace TankU.Unit.UnitStatus
{
    public class UnitStatusControl
    {
        Unit thisUnit;

        private int id;
        public float _rotateSpeed { private set; get; }

        public int _unitHealth { private set; get; }
        public float _unitSpeed { private set; get; }

        public int _bulletUse { private set; get; }
        public int _bombUse { private set; get; }

        public void Initial(Unit unit, int id)
        {
            thisUnit = unit;
            this.id = id;

            _unitHealth = 10;
            _unitSpeed = 3;
            _bulletUse = 0;
            _bombUse = 0;
            _rotateSpeed = 75f;
        }
        private void InitialOnTieBreak()
        {
            _unitHealth = 1;
            _unitSpeed = 4.5f;
            _bulletUse = 1;
            _rotateSpeed = 100;
        }

        public void ReduceHealth(int damage)
        {
            _unitHealth -= damage;
            if (_unitHealth <= 0) PublishSubscribe.Instance.Publish<MessageUnitDie>(
                new MessageUnitDie(id)
            );
        }
        public void AddHealth(int amount)
        {
            _unitHealth = Math.Clamp(_unitHealth + amount, 0, 5);
        }

        public void ChangeBullet(int bulletId)
        {
            _bulletUse = bulletId;
        }

        // public void InitialTieBreaker(MessegeTieBreaker messege)
        // {
        //    InitialOnTieBreak();
        // }
    }
}


