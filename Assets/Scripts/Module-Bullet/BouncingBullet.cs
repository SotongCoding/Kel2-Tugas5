using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using TankU.PubSub;
using UnityEngine;

namespace TankU.Bullet
{
    public class BouncingBullet : BaseBullet
    {
        [SerializeField] private bool _isBounceActive;
        [SerializeField] private int _playerID;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageBounceTimeUp>(MessageReciveBounceTimeUp);
        }
        private void OnDisable()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageBounceTimeUp>(MessageReciveBounceTimeUp);
        }

        public void SetPlayerID(int id)
        {
            _playerID = id;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            PublishSubscribe.Instance.Subscribe<MessageBounceTimeUp>(MessageReciveBounceTimeUp);
            _isBounceActive = true;
        }

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
            if (_isBounceActive == false)
            {
                if (other.gameObject.CompareTag("Wall"))
                {
                    StoreToPool();
                    PlayEffects();
                }
            }
        }

        private void MessageReciveBounceTimeUp(MessageBounceTimeUp message)
        {
            if (_playerID == message.unitId)
            {
                _isBounceActive = false;
            }
        }
    }
}