using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankU.PowerUp;
using Agate.MVC.Core;
using TankU.PubSub;
using System;

namespace TankU.Unit
{
    public class Unit : MonoBehaviour
    {
        //Testing
        [SerializeField]
        InputModule.KeyBoard.KeyBoardControl keyBoardControl;
        //======================================
        [Header("Identity")]
        [SerializeField] public int unitId;

        [Header("Shooting Setting")]
        public Transform bulletOutPos;
        public Transform head;

        //Global player Status Need
        public float _healthPrecentage => unitStatusControl._unitHealth / 5;

        UnitAction.UnitActionControl unitActionControl = new UnitAction.UnitActionControl();
        UnitStatus.UnitStatusControl unitStatusControl = new UnitStatus.UnitStatusControl();
        public UnitVisual.UnitVisualControl visualControl;


        private void Intial()
        {
            unitStatusControl.Initial(this, unitId);
            unitActionControl.Initial(this, unitStatusControl, visualControl);
        }
        private void SubscribeMessege()
        {
            PublishSubscribe.Instance.Subscribe<MessageTieBreaker>(unitStatusControl.InitialTieBreaker);
        }
        private void UnsubscribeMessege()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageTieBreaker>(unitStatusControl.InitialTieBreaker);
        }

        private void Start()
        {
            Intial();
            SubscribeMessege();

            //Testing
            SetController(keyBoardControl);
        }
        private void OnDisable()
        {
            UnsubscribeMessege();
        }

        private void Update()
        {
            unitActionControl.RunAction();

        }

        public void SetController(IUnitKeyAction keyControl)
        {
            unitActionControl.InitialControl(keyControl);
        }

        IEnumerator PowerUpEffectDuration(float duration, System.Action onDurationEnd)
        {
            yield return new WaitForSeconds(duration);
            onDurationEnd?.Invoke();
        }

        public void AddHealth()
        {
            unitStatusControl.AddHealth(1);
            Debug.Log("Heal");
        }
        public void ReciveBulletDamage()
        {
            unitStatusControl.ReduceHealth(1);
        }
        public void ReciveBombDamage()
        {
            unitStatusControl.ReduceHealth(2);
            Debug.Log("Recive Bomb Damage" + name);
        }
        public void BouncingBullet(float PUduration)
        {
            unitStatusControl.ChangeBullet(1);
            Debug.Log("Bounce");
            StartCoroutine(PowerUpEffectDuration(PUduration,
            () =>
            {
                unitStatusControl.ChangeBullet(0);
            }));
        }

        internal void CountDownShootBullet()
        {
            StartCoroutine(CountDown());
            IEnumerator CountDown()
            {
                yield return new WaitForSeconds(unitStatusControl._shootBullet_delay);
                unitStatusControl.SetShootStatus(true);
            }
        }
        internal void CountDownPlantBomb()
        {
            StartCoroutine(CountDown());
            IEnumerator CountDown()
            {
                yield return new WaitForSeconds(unitStatusControl._plantBomb_delay);
                unitStatusControl.SetPlantStatus(true);
            }
        }

    }
}
