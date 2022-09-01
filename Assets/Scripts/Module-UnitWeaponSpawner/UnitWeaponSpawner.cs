using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using TankU.Bullet;
using TankU.Bomb;
using UnityEngine;
using TankU.PubSub;
using TankU.PoolingSystem;

namespace TankU.UnitWeaponSpawner
{
    public class UnitWeaponSpawner : MonoBehaviour
    {
        PoolingSystem.PoolingSystem _baseBulletPool = new();
        PoolingSystem.PoolingSystem _bouncingBulletPool = new();
        PoolingSystem.PoolingSystem _bombPool = new();

        public BaseBullet _baseBullet;
        public BouncingBullet _bounceBullet;
        public Bomb.Bomb _bombBomb;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageSpawnBullet>(MessageReciveSpawnBullet);
            PublishSubscribe.Instance.Subscribe<MessageSpawnBomb>(MessageReciveSpawnBomb);
        }
        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageSpawnBullet>(MessageReciveSpawnBullet);
            PublishSubscribe.Instance.Unsubscribe<MessageSpawnBomb>(MessageReciveSpawnBomb);
        }

        private void Start()
        {
            _baseBullet = Resources.Load<BaseBullet>(@"Prefabs/Bullet");
            _bounceBullet = Resources.Load<BouncingBullet>(@"Prefabs/BouncingBullet");
            _bombBomb = Resources.Load<Bomb.Bomb>(@"Prefabs/Bomb");
        }

        private void MessageReciveSpawnBullet(MessageSpawnBullet message)
        {
            IPoolObject _bulletToSpawn;
            if (message.useBouncing)
            {
                _bulletToSpawn = CreateBouncingBullet(message.bulletOutPos.position, message.shooter.rotation);
                _bulletToSpawn.gameObject.GetComponent<BouncingBullet>().SetPlayerID(message.unitId);
            }
            else
            {
                _bulletToSpawn = CreateBaseBullet(message.bulletOutPos.position, message.shooter.rotation);
            }
            _bulletToSpawn.transform.SetPositionAndRotation(message.bulletOutPos.position, message.shooter.rotation);
        }

        private void MessageReciveSpawnBomb(MessageSpawnBomb message)
        {
            IPoolObject _bombToSpawn = CreateBomb(message.shooter.position);
        }

        public IPoolObject CreateBaseBullet(Vector3 position, Quaternion rotation)
        {
            IPoolObject createdBaseBullet = _baseBulletPool.CreateObject(_baseBullet, position, rotation);
            return createdBaseBullet;
        }

        public IPoolObject CreateBouncingBullet(Vector3 position, Quaternion rotation)
        {
            IPoolObject createdBouncingBullet = _bouncingBulletPool.CreateObject(_bounceBullet, position, rotation);
            return createdBouncingBullet;
        }

        public IPoolObject CreateBomb(Vector3 position)
        {
            IPoolObject createdBomb = _bombPool.CreateObject(_bombBomb, position);
            return createdBomb;
        }
    }
}