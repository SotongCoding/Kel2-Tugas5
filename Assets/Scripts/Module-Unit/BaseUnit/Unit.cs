using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankU.PowerUp;
using Agate.MVC.Core;
using TankU.PubSub;
using System;
using TankU.Bullet;
using TankU.Bomb;
using TankU.GameplayUI;

namespace TankU.Unit
{
    public class Unit : MonoBehaviour, IBulletHitAble, IBombHitAble
    {
        //Testing
        [SerializeField]
        InputModule.KeyBoard.KeyBoardControl keyBoardControl;
        //======================================
        [SerializeField] private int UnitId;
        public int unitId { get => UnitId; }

        [Header("Shooting Setting")]
        public Transform bulletOutPos;
        public Transform head;

        [Header("Color")]
        MeshRenderer bodyRender,
        headRender, frontRender;

        //Global player Status Need
        public int _health => unitStatusControl._unitHealth;
        public bool isGameStart = false;

        UnitAction.UnitActionControl unitActionControl = new UnitAction.UnitActionControl();
        UnitStatus.UnitStatusControl unitStatusControl = new UnitStatus.UnitStatusControl();
        public UnitVisual.UnitVisualControl visualControl;


        private void Intial()
        {
            unitStatusControl.Initial(this, unitId);
            unitActionControl.Initial(this, unitStatusControl, visualControl);
            visualControl.Initial(this);
            isGameStart = false;


        }
        private void SubscribeMessege()
        {
            PublishSubscribe.Instance.Subscribe<MessageTieBreaker>(unitStatusControl.InitialTieBreaker);
            PublishSubscribe.Instance.Subscribe<MessageStartGameplay>(GameStart);
        }
        private void UnsubscribeMessege()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageTieBreaker>(unitStatusControl.InitialTieBreaker);
            PublishSubscribe.Instance.Unsubscribe<MessageStartGameplay>(GameStart);
        }

        private void Start()
        {
            Intial();
            SubscribeMessege();
            //visualControl.SetUnitColor(Color.white,Color.cyan);

            //Testing
            SetController(keyBoardControl);
        }
        private void OnDisable()
        {
            UnsubscribeMessege();
        }

        private void Update()
        {
            if (isGameStart) unitActionControl.RunAction();

        }

        private IEnumerator PowerUpEffectDuration(float duration, System.Action onDurationEnd)
        {
            yield return new WaitForSeconds(duration);
            onDurationEnd?.Invoke();
        }
        private void GameStart(MessageStartGameplay message)
        {
            isGameStart = true;
        }

        public void SetController(IUnitKeyAction keyControl)
        {
            unitActionControl.InitialControl(keyControl);
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
                PublishSubscribe.Instance.Publish<MessageBounceTimeUp>(new MessageBounceTimeUp(unitId));
            }));
        }

        public void CountDownShootBullet()
        {
            StartCoroutine(CountDown());
            IEnumerator CountDown()
            {
                yield return new WaitForSeconds(unitStatusControl._shootBullet_delay);
                unitStatusControl.SetShootStatus(true);
            }
        }
        public void CountDownPlantBomb()
        {
            StartCoroutine(CountDown());
            IEnumerator CountDown()
            {
                yield return new WaitForSeconds(unitStatusControl._plantBomb_delay);
                unitStatusControl.SetPlantStatus(true);
            }
        }

        public void SetUnitColor(Color mainColor, Color subColor)
        {
            visualControl.SetUnitColor(mainColor, subColor);

        }

    }
}
