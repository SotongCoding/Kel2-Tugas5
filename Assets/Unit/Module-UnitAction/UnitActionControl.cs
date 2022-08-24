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

        public void InitialUnit(Unit unit)
        {
            thisUnit = unit;
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
            if (KeyAction.RotateLeft) thisUnit.transform.Translate(Vector3.left * Time.deltaTime);
            else if (KeyAction.MoveRight) thisUnit.transform.Translate(Vector3.right * Time.deltaTime);
        }

        public void ShootBullet()
        {

        }
        public void PlaceBomb()
        {

        }
    }
}
