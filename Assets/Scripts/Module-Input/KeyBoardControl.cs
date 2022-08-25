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
        public override bool _moveUp => Input.GetKey(keyControl.moveUp);
        public override bool _moveDown => Input.GetKey(keyControl.moveDown);
        public override bool _moveLeft => Input.GetKey(keyControl.moveLeft);
        public override bool _moveRight => Input.GetKey(keyControl.moveRight);

        //Rotate
        public override bool _rotateLeft => Input.GetKey(keyControl.rotateLeft);
        public override bool _rotateRight => Input.GetKey(keyControl.rotateRight);

        //Shoot and Plant Bomb
        public override bool _shootBullet => Input.GetKeyDown(keyControl.shootBullet);
        public override bool _placeBomb => Input.GetKeyDown(keyControl.placeBomb);


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
