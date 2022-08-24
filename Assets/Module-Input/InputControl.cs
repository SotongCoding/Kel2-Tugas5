using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.InputModule
{
    public class InputControl : ScriptableObject, IUnitKeyAction
    {
        public virtual bool MoveUp => false;

        public virtual bool MoveDown => false;

        public virtual bool MoveLeft => false;

        public virtual bool MoveRight => false;

        public virtual bool RotateLeft => false;

        public virtual bool RotateRight => false;

        public virtual bool ShootBullet => false;

        public virtual bool PlaceBomb => false;
    }
}
