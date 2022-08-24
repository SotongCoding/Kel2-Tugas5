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

        public override bool MoveUp => Input.GetKey(keyControl.moveUp);

        public override bool MoveDown => Input.GetKey(keyControl.moveDown);

        public override bool MoveLeft => Input.GetKey(keyControl.moveLeft);

        public override bool MoveRight => Input.GetKey(keyControl.moveRight);

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
            public KeyCode shootPlaceBomb;

        }
    }
}
