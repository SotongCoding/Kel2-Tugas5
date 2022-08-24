using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.Unit
{
    public class Unit : MonoBehaviour
    {
        //Testing
        [SerializeField]
        InputModule.InputControl control;

        UnitAction.UnitActionControl unitActionControl = new UnitAction.UnitActionControl();

        private void Start()
        {
            Intial();

            //Testing
            SetController(control.Player1_Key);
        }

        private void Update() {
            unitActionControl.RunAction();
        }

        void Intial()
        {
            unitActionControl.InitialUnit(this);
        }

        public void SetController(InputModule.InputControl.UnitKeyControl keyControl)
        {
            unitActionControl.InitialControl(keyControl);
        }
    }
}
