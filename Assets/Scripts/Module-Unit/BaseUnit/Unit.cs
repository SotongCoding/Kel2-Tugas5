using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankU.PowerUp;
using Agate.MVC.Core;

namespace TankU.Unit
{
    public class Unit : MonoBehaviour
    {
        //Testing
        [SerializeField]
        InputModule.KeyBoard.KeyBoardControl keyBoardControl;
        //======================================
        [Header("Identity")]
        [SerializeField] int unitId;

        [Header("Shooting Setting")]
        public Transform bulletOutPos;
        public Transform head;

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
            //PublishSubscribe.Instance.Subscribe<MessegeTieBreaker>(unitStatusControl.InitialTieBreaker);
        }
        private void UnsubscribeMessege()
        {
            // PublishSubscribe.Instance.Unsubscribe<MessegeTieBreaker>(unitStatusControl.InitialTieBreaker);
        }

        private void Start()
        {
            Intial();
            SubscribeMessege();

            //Testing
            SetController(keyBoardControl);
        }
        private void OnDestroy()
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

        public void addHealth()
        {
            unitStatusControl.AddHealth(1);
        }

        public void BouncingBullet(float PUduration)
        {
            unitStatusControl.ChangeBullet(1);

            StartCoroutine(PowerUpEffectDuration(PUduration,
            () =>
            {
                unitStatusControl.ChangeBullet(0);
            }));
            }
    }
}
