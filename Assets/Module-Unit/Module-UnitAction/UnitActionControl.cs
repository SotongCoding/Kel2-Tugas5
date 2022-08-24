using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TankU.InputModule;

namespace TankU.Unit.UnitAction
{
    public class UnitActionControl
    {
        private IUnitKeyAction KeyAction;
        private Unit thisUnit;
        private UnitStatus.UnitStatusControl unitStatus;

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

            if (KeyAction._shootBullet)
            {
                ShootBullet();
            }
            if (KeyAction._placeBomb)
            {
                PlaceBomb();
            }
        }

        public void Move()
        {
            if (KeyAction._moveUp) thisUnit.transform.Translate(Vector3.forward * Time.deltaTime);
            else if (KeyAction._moveDown) thisUnit.transform.Translate(Vector3.back * Time.deltaTime);

            else if (KeyAction._moveLeft) thisUnit.transform.Translate(Vector3.left * Time.deltaTime);
            else if (KeyAction._moveRight) thisUnit.transform.Translate(Vector3.right * Time.deltaTime);

        }
        public void Rotate()
        {
            if (KeyAction._rotateLeft) thisUnit.head.Rotate(Vector3.up, -unitStatus._rotateSpeed * Time.deltaTime);
            else if (KeyAction._rotateRight) thisUnit.head.Rotate(Vector3.up, unitStatus._rotateSpeed * Time.deltaTime);
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
