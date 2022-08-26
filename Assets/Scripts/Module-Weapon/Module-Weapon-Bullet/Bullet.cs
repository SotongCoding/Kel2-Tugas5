using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TankU.PubSub;

namespace TankU.Weapon.Bullet
{
    public class Bullet : Weapon
    {
        [SerializeField] protected int _moveSpeed;
        protected Rigidbody _rigidbody;
        protected Collider _collider;

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
    }

    public interface IHitAbleObject{
        void HitEvent();
    }
}