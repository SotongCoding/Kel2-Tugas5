using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Agate.MVC.Core;
using System;

namespace TankU.Unit.UnitStatus
{
    public class UnitStatusControl
    {
        Unit thisUnit;

        int id;
        public float rotateSpeed { private set; get; }

        public int unitHealth { private set; get; }
        public float unitSpeed { private set; get; }

        public int bulletUse { private set; get; }
        public int bombUse { private set; get; }

        public void Initial(Unit unit, int id)
        {
            thisUnit = unit;
            this.id = id;

            unitHealth = 10;
            unitSpeed = 3;
            bulletUse = 0;
            bombUse = 0;
            rotateSpeed = 75f;
        }
        private void InitialOnTieBreak()
        {
            unitHealth = 1;
            unitSpeed = 4.5f;
            bulletUse = 1;
            rotateSpeed = 100;
        }
        
        public void ReduceHealth()
        {
            unitHealth -= 1;
            if (unitHealth <= 0) PublishSubscribe.Instance.Publish<MessegeUnitDie>(
                new MessegeUnitDie(id)
            );
        }
        public void AddHealth(int amount)
        {
            unitHealth += amount;
        }

        public void ChangeBullet(int bulletId)
        {
            bulletUse = bulletId;
        }

        // public void InitialTieBreaker(MessegeTieBreaker messege)
        // {
        //    InitialOnTieBreak();
        // }
    }
}

public struct MessegeUnitDie
{
    public int unitId;

    public MessegeUnitDie(int unitId)
    {
        this.unitId = unitId;
    }
}
