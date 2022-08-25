using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TankU.PubSub;

namespace TankU.Weapon.Bullet
{
    public class Bullet : Weapon
    {
        [SerializeField] private int _moveSpeed;
        private Rigidbody _rigidbody;
        private Collider _collider;
        private bool _isBouncing;

        private void OnEnable()
        {
            MoveBullet();
            //PublishSubscribe.Instance.Subscribe<MakeBouncingMessage>(MakeBouncing);
            //PublishSubscribe.Instance.Subscribe<MakeNotBouncingMessage>(MakeNotBouncing);
        }
        private void OnDisable()
        {
            //PublishSubscribe.Instance.Unsubscribe<MakeBouncingMessage>(MakeBouncing);
            //PublishSubscribe.Instance.Unsubscribe<MakeNotBouncingMessage>(MakeNotBouncing);
        }

        void Start()
        {
            _isBouncing = false;
            _moveSpeed = 5;
            SetPhysicsMaterial();
            MoveBullet();
        }

        private void MoveBullet()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = transform.forward * _moveSpeed;
        }

        //private void MakeBouncing(MakeBouncingMessage message)
        //{
        //    _collider.material.bounciness = 1;
        //    _isBouncing = true;
        //}

        //private void MakeNotBouncing(MakeNotBouncingMessage message)
        //{
        //    _collider.material.bounciness = 0;
        //    _isBouncing = false;
        //}

        private void SetPhysicsMaterial()
        {
            _collider = GetComponent<Collider>();
            _collider.material.dynamicFriction = 0;
            _collider.material.staticFriction = 0;
            _collider.material.bounceCombine = PhysicMaterialCombine.Maximum;
            _collider.material.frictionCombine = PhysicMaterialCombine.Minimum;
        }

        protected override void OnHitWall(Collision other)
        {
            if (_isBouncing == false)
            {
                if (other.gameObject.CompareTag("Wall"))
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}