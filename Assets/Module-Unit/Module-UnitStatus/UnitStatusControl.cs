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
        public int unitHealth { private set; get; }
        public float unitSpeed { private set; get; }

        public int bulletUse { private set; get; }
        public int bombUse { private set; get; }

        public void IntialStatus(Unit unit)
        {
            thisUnit = unit;

            unitHealth = 10;
            unitSpeed = 3;
            bulletUse = 0;
            bombUse = 0;
        }
        void InitialTieBreak()
        {
            unitHealth = 1;
            unitSpeed = 4.5f;
            bulletUse = 1;
        }
        public void ReduceHealth()
        {
            unitHealth -= 1;
            if (unitHealth <= 0) PublishSubscribe.Instance.Publish<MessegeUnitDie>(new MessegeUnitDie(thisUnit));

        }

        // internal void InitialTieBreaker(MessegeTieBreaker messege)
        // {
        //    InitialTieBreak();
        // }
    }
}

public struct MessegeUnitDie
{
    public TankU.Unit.Unit unitDie;

    public MessegeUnitDie(TankU.Unit.Unit unitDie)
    {
        this.unitDie = unitDie;
    }
}
