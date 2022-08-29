using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankU.PubSub;
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

            if (KeyAction._shootBullet && unitStatus._canShoot)
            {
                ShootBullet();
            }
            if (KeyAction._placeBomb && unitStatus._canPlant)
            {
                PlaceBomb();
            }
        }

        public void Move()
        {
            Vector3 dir = thisUnit.transform.position;

            if (KeyAction._moveUp) { thisUnit.transform.Translate(Vector3.forward * unitStatus._unitSpeed * Time.deltaTime); dir = thisUnit.transform.forward; }
            else if (KeyAction._moveDown) { thisUnit.transform.Translate(Vector3.back * unitStatus._unitSpeed * Time.deltaTime); dir = -thisUnit.transform.forward; }

            else if (KeyAction._moveLeft) { thisUnit.transform.Translate(Vector3.left * unitStatus._unitSpeed * Time.deltaTime); dir = -thisUnit.transform.right; }
            else if (KeyAction._moveRight) { thisUnit.transform.Translate(Vector3.right * unitStatus._unitSpeed * Time.deltaTime); dir = thisUnit.transform.right; }

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
                new MessageSpawnBullet(thisUnit.head.transform, thisUnit.bulletOutPos,
                unitStatus._bulletUse == 1, unitStatus._id));
            PublishSubscribe.Instance.Publish<MessageSoundfx>(new MessageSoundfx("shoot"));

            thisUnit.CountDownShootBullet();
            unitStatus.SetShootStatus(false);
        }
        public void PlaceBomb()
        {
            if (unitStatus._bombAmount <= 0) return;

            PublishSubscribe.Instance.Publish<MessageSpawnBomb>(
                new MessageSpawnBomb(thisUnit.transform, unitStatus._id));

            thisUnit.CountDownPlantBomb();
            unitStatus.SetPlantStatus(false);
            unitStatus.ReduceBomb();
        }
    }
}
