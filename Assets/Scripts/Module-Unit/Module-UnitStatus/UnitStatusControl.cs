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

        public int _id { private set; get; }
        public float _rotateSpeed { private set; get; }

        public int _unitHealth { private set; get; }
        public float _unitSpeed { private set; get; }

        public int _bulletUse { private set; get; }
        public int _bombUse { private set; get; }

        public int _bombAmount { private set; get; }

        public float _shootBullet_delay { private set; get; }
        public float _plantBomb_delay { private set; get; }

        public bool _canShoot { get; private set; } = true;
        public bool _canPlant { get; private set; } = true;



        public void Initial(Unit unit, int id)
        {
            thisUnit = unit;
            _id = id;

            _unitHealth = 10;
            _unitSpeed = 3;
            _rotateSpeed = 200;

            _bulletUse = 0;
            _bombUse = 0;

            _shootBullet_delay = 1.5f;
            _plantBomb_delay = 5;

            _bombAmount = 5;
        }
        private void InitialOnTieBreak()
        {
            _unitHealth = 1;
            _unitSpeed = 4.5f;
            _bulletUse = 1;
            _rotateSpeed = 300;

            _shootBullet_delay = 0.75f;
            _plantBomb_delay = 2.5f;
            MonoBehaviour.FindObjectOfType<GameplayUI.GameplayUI>().UpdateHealth(_id);

        }

        public void ReduceHealth(int damage)
        {
            _unitHealth -= damage;
            if (_unitHealth <= 0)
            {
                PublishSubscribe.Instance.Publish<MessageUnitDie>(
                    new MessageUnitDie(thisUnit)
                );

                thisUnit.gameObject.SetActive(false);
            }
            MonoBehaviour.FindObjectOfType<GameplayUI.GameplayUI>().UpdateHealth(_id);
        }
        public void ReduceBomb()
        {
            _bombAmount--;
        }
        public void AddHealth(int amount)
        {
            _unitHealth = Math.Clamp(_unitHealth + amount, 0, 10);
            MonoBehaviour.FindObjectOfType<GameplayUI.GameplayUI>().UpdateHealth(_id);
        }

        public void ChangeBullet(int bulletId)
        {
            _bulletUse = bulletId;
        }

        public void InitialTieBreaker(MessageTieBreaker messege)
        {
            InitialOnTieBreak();
        }
        public void SetShootStatus(bool canShoot)
        {
            _canShoot = canShoot;
        }
        public void SetPlantStatus(bool canPlant)
        {
            _canPlant = canPlant;
        }
    }
}


