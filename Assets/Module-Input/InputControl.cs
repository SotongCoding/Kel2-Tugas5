using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.InputControl
{
    [CreateAssetMenu(menuName = "Unit Control", fileName = "Control Key")]
    public class InputControl : ScriptableObject
    {

        [SerializeField]
        UnitKeyControl player1_Key,
        player2_Key;

        public UnitKeyControl Player1_Key
        {
            get
            {
                return player1_Key;
            }
        }
        public UnitKeyControl Player2_Key
        {
            get
            {
                return player1_Key;
            }
        }


        [System.Serializable]
        public struct UnitKeyControl
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
