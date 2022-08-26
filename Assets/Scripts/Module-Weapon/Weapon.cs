using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankU.PubSub;
using TankU.Weapon.Bullet;

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
                collision.gameObject.GetComponent<Unit.Unit>().ReciveBulletDamage();
                gameObject.SetActive(false);
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

}