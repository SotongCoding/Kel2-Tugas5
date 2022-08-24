using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Agate.MVC.Core;

namespace TankU.Unit
{
    public class Unit : MonoBehaviour
    {
        //Testing
        [SerializeField]
        InputModule.KeyBoard.KeyBoardControl keyBoardControl;

        UnitAction.UnitActionControl unitActionControl = new UnitAction.UnitActionControl();
        UnitStatus.UnitStatusControl unitStatusControl = new UnitStatus.UnitStatusControl();

        void InitialMessege()
        {
            //PublishSubscribe.Instance.Subscribe<MessegeTieBreaker>(unitStatusControl.InitialTieBreaker);
        }

        private void Start()
        {
            Intial();
            InitialMessege();

            //Testing
            SetController(keyBoardControl);
        }

        private void Update()
        {
            unitActionControl.RunAction();

        }

        void Intial()
        {
            unitActionControl.InitialUnit(this);
            unitStatusControl.IntialStatus(this);
        }


        public void SetController(IUnitKeyAction keyControl)
        {
            unitActionControl.InitialControl(keyControl);
        }
    }
}
