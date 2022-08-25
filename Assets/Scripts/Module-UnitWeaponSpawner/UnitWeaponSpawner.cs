using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using TankU.Weapon.Bullet;
using UnityEngine;

namespace TankU.UnitWeaponSpawner
{
    public class UnitWeaponSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _pooledBullet;
        [SerializeField] private List<GameObject> _pooledBomb;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private GameObject _bomb;
        [SerializeField] private int _bulletAmountToPool;
        [SerializeField] private int _bombAmountToPool;
        private float _spawnBulletCooldown_Running;
        private float _spawnBombCooldown_Running;
        [SerializeField] private float _spawnBulletCooldown_Duration;
        [SerializeField] private float _spawnBombCooldown_Duration;
        private Bullet _bulletScript;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageSpawnBullet>(ReciveMessegeSpawnBullet);
            PublishSubscribe.Instance.Subscribe<MessageSpawnBomb>(ReciveMessegeSpawnBomb);
        }
        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<MessageSpawnBullet>(ReciveMessegeSpawnBullet);
            PublishSubscribe.Instance.Unsubscribe<MessageSpawnBomb>(ReciveMessegeSpawnBomb);
        }

        private void Start()
        {
            _pooledBullet = new List<GameObject>();
            _pooledBomb = new List<GameObject>();
            _bullet = Resources.Load<GameObject>(@"Prefabs/Bullet");
            _bomb = Resources.Load<GameObject>(@"Prefabs/Bomb");
            _bulletAmountToPool = 7;
            _bombAmountToPool = 5;
            _spawnBulletCooldown_Duration = 2f;
            _spawnBombCooldown_Duration = 2f;
            _spawnBulletCooldown_Running = _spawnBulletCooldown_Duration;
            _spawnBombCooldown_Running = _spawnBombCooldown_Duration;

            for (int i = 0; i < _bulletAmountToPool; i++)
            {
                GameObject obj = Instantiate(_bullet, transform.position, _bullet.transform.rotation);
                _bulletScript = obj.AddComponent<Bullet>();
                obj.SetActive(false);
                _pooledBullet.Add(obj);
            }

            for (int i = 0; i < _bombAmountToPool; i++)
            {
                GameObject obj = Instantiate(_bomb, transform.position, Quaternion.identity);
                obj.SetActive(false);
                _pooledBomb.Add(obj);
            }
        }

        private void Update()
        {
            if (_spawnBulletCooldown_Running < _spawnBulletCooldown_Duration)
            {
                _spawnBulletCooldown_Running += Time.deltaTime;
            }
            if (_spawnBombCooldown_Running < _spawnBombCooldown_Duration)
            {
                _spawnBombCooldown_Running += Time.deltaTime;
            }
        }

        private GameObject GetPooledBullet()
        {
            for (int i = 0; i < _bulletAmountToPool; i++)
            {
                if (!_pooledBullet[i].activeInHierarchy)
                {
                    return _pooledBullet[i];
                }
            }
            return null;
        }

        private GameObject GetPooledBomb()
        {
            for (int i = 0; i < _bombAmountToPool; i++)
            {
                if (!_pooledBomb[i].activeInHierarchy)
                {
                    return _pooledBomb[i];
                }
            }
            return null;
        }

        private void ReciveMessegeSpawnBullet(MessageSpawnBullet message)
        {
            if (_spawnBulletCooldown_Running >= _spawnBulletCooldown_Duration)
            {
                GameObject _bulletToSpawn = GetPooledBullet();
                _bulletToSpawn.transform.SetPositionAndRotation(message.outPos, message.unit.rotation);
                _bulletToSpawn.SetActive(true);
                _spawnBulletCooldown_Running = 0;
            }
        }

        private void ReciveMessegeSpawnBomb(MessageSpawnBomb message)
        {
            if (_spawnBombCooldown_Running >= _spawnBombCooldown_Duration)
            {
                GameObject _bombToSpawn = GetPooledBomb();
                _bombToSpawn.transform.SetPositionAndRotation(message.unit.position + Vector3.down, Quaternion.identity);
                _bombToSpawn.SetActive(true);
                _spawnBombCooldown_Running = 0;
            }
        }
    }
}