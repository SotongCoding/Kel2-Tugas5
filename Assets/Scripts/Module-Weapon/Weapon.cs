using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        protected int damagePoint = 1;

        private void OnCollisionEnter(Collision collision)
        {
            OnHitWall(collision);
            if (collision.gameObject.CompareTag("Player"))
            {
                PublishSubscribe.Instance.Publish<Hit>(new Hit(damagePoint));
            }
        }

        protected virtual void OnHitWall(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                gameObject.SetActive(false);
            }
        }
    }

    public struct Hit
    {
        int _hitDamage;

        public Hit(int damagePoint)
        {
            this._hitDamage = damagePoint;
        }
    }
}