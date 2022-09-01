using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.InputModule
{
    public class InputControl : ScriptableObject, IUnitKeyAction
    {
        public virtual bool _moveUp => false;

        public virtual bool _moveDown => false;

        public virtual bool _moveLeft => false;

        public virtual bool _moveRight => false;

        public virtual bool _rotateLeft => false;

        public virtual bool _rotateRight => false;

        public virtual bool _shootBullet => false;

        public virtual bool _placeBomb => false;
    }
}
