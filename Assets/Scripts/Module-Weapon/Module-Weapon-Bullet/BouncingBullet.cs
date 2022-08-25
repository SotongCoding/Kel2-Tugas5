using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.Weapon.Bullet
{
    public class BouncingBullet : Bullet
    {
        protected override void SetPhysicsMaterial()
        {
            base.SetPhysicsMaterial();
            _collider.material.bounciness = 1;
            _collider.material.dynamicFriction = 0;
            _collider.material.staticFriction = 0;
            _collider.material.bounceCombine = PhysicMaterialCombine.Maximum;
            _collider.material.frictionCombine = PhysicMaterialCombine.Minimum;
        }

        protected override void OnHitWall(Collision other)
        {
            if (_collider.material.bounciness == 0)
            {
                if (other.gameObject.CompareTag("Wall"))
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}