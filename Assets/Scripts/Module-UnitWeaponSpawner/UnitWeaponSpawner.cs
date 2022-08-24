using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
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
        private float _spawnBulletCooldownRunning;
        private float _spawnBombCooldownRunning;
        [SerializeField] private float _spawnBulletCooldownDuration;
        [SerializeField] private float _spawnBombCooldownDuration;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<SpawnBulletMessage>(SpawnBullet);
            PublishSubscribe.Instance.Subscribe<SpawnBombMessage>(SpawnBomb);
        }
        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<SpawnBulletMessage>(SpawnBullet);
            PublishSubscribe.Instance.Unsubscribe<SpawnBombMessage>(SpawnBomb);
        }

        private void Start()
        {
            _pooledBullet = new List<GameObject>();
            _pooledBomb = new List<GameObject>();
            _bullet = Resources.Load<GameObject>(@"Prefabs/Bullet");
            _bomb = Resources.Load<GameObject>(@"Prefabs/Bomb");
            _bulletAmountToPool = 10;
            _bombAmountToPool = 10;
            _spawnBulletCooldownDuration = 2f;
            _spawnBombCooldownDuration = 2f;
            _spawnBulletCooldownRunning = _spawnBulletCooldownDuration;
            _spawnBombCooldownRunning = _spawnBombCooldownDuration;

            for (int i = 0; i < _bulletAmountToPool; i++)
            {
                GameObject obj = Instantiate(_bullet, transform.position, Quaternion.identity);
                obj.SetActive(false);
                _pooledBullet.Add(obj);
                obj.transform.parent = gameObject.transform;
            }

            for (int i = 0; i < _bombAmountToPool; i++)
            {
                GameObject obj = Instantiate(_bomb, transform.position, Quaternion.identity);
                obj.SetActive(false);
                _pooledBomb.Add(obj);
                obj.transform.parent = gameObject.transform;
            }
        }

        private void Update()
        {
            if (_spawnBulletCooldownRunning < _spawnBulletCooldownDuration)
            {
                _spawnBulletCooldownRunning += Time.deltaTime;
            }
            if (_spawnBombCooldownRunning < _spawnBombCooldownDuration)
            {
                _spawnBombCooldownRunning += Time.deltaTime;
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

        private void SpawnBullet(SpawnBulletMessage message)
        {
            if (_spawnBulletCooldownRunning >= _spawnBulletCooldownDuration)
            {
                GameObject _bulletToSpawn = GetPooledBullet();
                _bulletToSpawn.SetActive(true);
                _spawnBulletCooldownRunning = 0;
            }
        }

        private void SpawnBomb(SpawnBombMessage message)
        {
            if (_spawnBombCooldownRunning >= _spawnBombCooldownDuration)
            {
                GameObject _bombToSpawn = GetPooledBomb();
                _bombToSpawn.SetActive(true);
                _spawnBombCooldownRunning = 0;
            }
        }
    }
}