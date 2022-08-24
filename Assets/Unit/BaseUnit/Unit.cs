using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.Unit
{
    public class Unit : MonoBehaviour
    {
        //Testing
        [SerializeField]
        InputModule.KeyBoard.KeyBoardControl keyBoardControl;

        UnitAction.UnitActionControl unitActionControl = new UnitAction.UnitActionControl();

        private void Start()
        {
            Intial();

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
        }

        public void SetController(IUnitKeyAction keyControl)
        {
            unitActionControl.InitialControl(keyControl);
        }
    }
}
