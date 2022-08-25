using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Agate.MVC.Core;

namespace TankU.Unit.UnitAction
{
    public class UnitActionControl
    {
        private IUnitKeyAction KeyAction;
        private Unit thisUnit;
        private UnitStatus.UnitStatusControl unitStatus;
        private UnitVisual.UnitVisualControl unitVisual;

        public void Initial(Unit unit, UnitStatus.UnitStatusControl statusControl, UnitVisual.UnitVisualControl unitVisual)
        {
            thisUnit = unit;
            unitStatus = statusControl;
            this.unitVisual = unitVisual;
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
            Vector3 dir = thisUnit.transform.position;

            if (KeyAction._moveUp) { thisUnit.transform.Translate(Vector3.forward * Time.deltaTime); dir = thisUnit.transform.forward; }
            else if (KeyAction._moveDown) { thisUnit.transform.Translate(Vector3.back * Time.deltaTime); dir = -thisUnit.transform.forward; }

            else if (KeyAction._moveLeft) { thisUnit.transform.Translate(Vector3.left * Time.deltaTime); dir = -thisUnit.transform.right; }
            else if (KeyAction._moveRight) { thisUnit.transform.Translate(Vector3.right * Time.deltaTime); dir = thisUnit.transform.right; }

            if (KeyAction._moveUp || KeyAction._moveDown || KeyAction._moveLeft || KeyAction._moveRight)
            {
                unitVisual.PlayVisual_Move(dir);
            }
            else
                unitVisual.PlayVisual_Idle();

        }
        public void Rotate()
        {
            if (KeyAction._rotateLeft) thisUnit.head.Rotate(Vector3.up, -unitStatus._rotateSpeed * Time.deltaTime);
            else if (KeyAction._rotateRight) thisUnit.head.Rotate(Vector3.up, unitStatus._rotateSpeed * Time.deltaTime);
        }

        public void ShootBullet()
        {
            PublishSubscribe.Instance.Publish<MessageSpawnBullet>(
                new MessageSpawnBullet(thisUnit.transform, thisUnit.bulletOutPos.position));
        }
        public void PlaceBomb()
        {
            PublishSubscribe.Instance.Publish<MessageSpawnBomb>(
                new MessageSpawnBomb(thisUnit.transform));
        }
    }
}
