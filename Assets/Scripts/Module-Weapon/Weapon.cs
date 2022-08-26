using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankU.PubSub;

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
                var hitTarget = collision.gameObject.GetComponent<Unit.Unit>();
                hitTarget.ReciveBulletDamage();
                gameObject.SetActive(false);
                PublishSubscribe.Instance.Publish<MessageVfx>(new MessageVfx("bullet_explosion", transform.position));
            }
        }

        protected virtual void OnHitWall(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                gameObject.SetActive(false);
                PublishSubscribe.Instance.Publish<MessageVfx>(new MessageVfx("bullet_explosion", transform.position));
            }
        }
    }

}