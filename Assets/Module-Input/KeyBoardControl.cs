using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.InputModule.KeyBoard
{
    [CreateAssetMenu(menuName = "Unit Control/Keyboard", fileName = "KeyboardControl")]
    public class KeyBoardControl : InputControl
    {
        [SerializeField]
        KeyControl keyControl;

        //Move
        public override bool MoveUp => Input.GetKey(keyControl.moveUp);
        public override bool MoveDown => Input.GetKey(keyControl.moveDown);
        public override bool MoveLeft => Input.GetKey(keyControl.moveLeft);
        public override bool MoveRight => Input.GetKey(keyControl.moveRight);

        //Rotate
        public override bool RotateLeft => Input.GetKey(keyControl.rotateLeft);
        public override bool RotateRight => Input.GetKey(keyControl.rotateRight);

        //Shoot and Plant Bomb
        public override bool ShootBullet => Input.GetKeyDown(keyControl.shootBullet);
        public override bool PlaceBomb => Input.GetKeyDown(keyControl.placeBomb);


        [System.Serializable]
        public struct KeyControl
        {
            public KeyCode moveUp;
            public KeyCode moveDown;
            public KeyCode moveLeft;
            public KeyCode moveRight;

            public KeyCode rotateLeft;
            public KeyCode rotateRight;

            public KeyCode shootBullet;
            public KeyCode placeBomb;

        }
    }
}
