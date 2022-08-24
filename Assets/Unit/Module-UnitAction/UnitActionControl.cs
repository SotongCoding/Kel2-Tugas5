using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TankU.InputModule;

namespace TankU.Unit.UnitAction
{
    public class UnitActionControl
    {
        InputControl.UnitKeyControl keyControl;
        Unit thisUnit;

        public void InitialUnit(Unit unit)
        {
            thisUnit = unit;
        }
        public void InitialControl(InputControl.UnitKeyControl keyControl)
        {
            this.keyControl = keyControl;
        }

        public void RunAction()
        {
            Move();
            Rotate();

            if (Input.GetKeyDown(keyControl.shootBullet))
            {
                ShootBullet();
            }
            if (Input.GetKeyDown(keyControl.shootPlaceBomb))
            {
                PlaceBomb();
            }
        }

        public void Move()
        {
            if (Input.GetKey(keyControl.moveUp)) thisUnit.transform.Translate(Vector3.forward * Time.deltaTime);
            else if (Input.GetKey(keyControl.moveDown)) thisUnit.transform.Translate(Vector3.back * Time.deltaTime);

            else if (Input.GetKey(keyControl.moveLeft)) thisUnit.transform.Translate(Vector3.left * Time.deltaTime);
            else if (Input.GetKey(keyControl.moveRight)) thisUnit.transform.Translate(Vector3.right * Time.deltaTime);

        }
        public void Rotate()
        {
            if (Input.GetKey(keyControl.rotateLeft)) thisUnit.transform.Translate(Vector3.left * Time.deltaTime);
            else if (Input.GetKey(keyControl.rotateRight)) thisUnit.transform.Translate(Vector3.right * Time.deltaTime);
        }

        public void ShootBullet()
        {

        }
        public void PlaceBomb()
        {

        }
    }
}
