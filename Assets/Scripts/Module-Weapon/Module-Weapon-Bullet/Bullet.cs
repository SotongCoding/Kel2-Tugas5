using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.Weapon.Bullet
{
    public class Bullet : Weapon
    {
        [SerializeField] private int _moveSpeed;
        private GameObject _spawner;
        private Rigidbody _rigidbody;
        private Collider _collider;
        private bool _isBouncing;
        
        private Vector3 defaultPosition;

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

        private void Awake()
        {
            _spawner = GameObject.Find("UnitWeaponSpawner");
        }

        void Start()
        {
            _isBouncing = false;
            _moveSpeed = 5;
            SetPhysicsMaterial();
            MoveBullet();
        }

        private void Update()
        {
            //defaultPosition = _spawner.transform.position;
        }

        private void MoveBullet()
        {
            transform.position = _spawner.transform.position;
            transform.rotation = _spawner.transform.rotation;
            //transform.Rotate(90, 0, 0);
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.velocity = new Vector3(0, 0, _moveSpeed * 1);
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