using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TankU.InputModule;

namespace TankU.Unit.UnitAction
{
    public class UnitActionControl
    {
        IUnitKeyAction KeyAction;
        Unit thisUnit;
        UnitStatus.UnitStatusControl unitStatus;

        public void Initial(Unit unit, UnitStatus.UnitStatusControl statusControl)
        {
            thisUnit = unit;
            unitStatus = statusControl;
        }
        public void InitialControl(IUnitKeyAction keyAction)
        {
            this.KeyAction = keyAction;
        }

        public void RunAction()
        {
            Move();
            Rotate();

            if (KeyAction.ShootBullet)
            {
                ShootBullet();
            }
            if (KeyAction.PlaceBomb)
            {
                PlaceBomb();
            }
        }

        public void Move()
        {
            if (KeyAction.MoveUp) thisUnit.transform.Translate(Vector3.forward * Time.deltaTime);
            else if (KeyAction.MoveDown) thisUnit.transform.Translate(Vector3.back * Time.deltaTime);

            else if (KeyAction.MoveLeft) thisUnit.transform.Translate(Vector3.left * Time.deltaTime);
            else if (KeyAction.MoveRight) thisUnit.transform.Translate(Vector3.right * Time.deltaTime);

        }
        public void Rotate()
        {
            if (KeyAction.RotateLeft) thisUnit.head.Rotate(Vector3.up, -unitStatus.rotateSpeed * Time.deltaTime);
            else if (KeyAction.RotateRight) thisUnit.head.Rotate(Vector3.up, unitStatus.rotateSpeed * Time.deltaTime);
        }

        public void ShootBullet()
        {
            //Spawner.CreateBullet(thisUnit.bulletOutPos, unitStatus.bulletUse);
        }
        public void PlaceBomb()
        {
            //Spawner.CreateBullet(thisUnit.bulletOutPos, unitStatus.bulletUse);
        }
    }
}
