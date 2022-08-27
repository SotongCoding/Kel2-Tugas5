using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TankU.PubSub;

namespace TankU.Bullet
{
    public class BaseBullet : MonoBehaviour
    {
        [SerializeField] protected int _moveSpeed;
        protected Rigidbody _rigidbody;
        protected Collider _collider;
        protected int damagePoint = 1;

        protected virtual void OnEnable()
        {
            MoveBullet();
        }

        protected virtual void Start()
        {
            _moveSpeed = 5;
            SetPhysicsMaterial();
            MoveBullet();
        }

        protected virtual void MoveBullet()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = transform.forward * _moveSpeed;
        }

        protected virtual void SetPhysicsMaterial()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            OnHitWall(collision);
            if (collision.gameObject.CompareTag("Player"))
            {
                var hitTarget = collision.gameObject.GetComponent<Unit.Unit>();
                if (hitTarget.TryGetComponent(out IBulletHitAble bulletHitAble))
                {
                    hitTarget.ReciveBulletDamage();
                    gameObject.SetActive(false);
                    PublishSubscribe.Instance.Publish<MessageVfx>(new MessageVfx("bullet_explosion", transform.position));
                }
                //hitTarget.ReciveBulletDamage();
                //gameObject.SetActive(false);
                //PublishSubscribe.Instance.Publish<MessageVfx>(new MessageVfx("bullet_explosion", transform.position));
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

    public interface IBulletHitAble
    {
        void ReciveBulletDamage();
    }
}