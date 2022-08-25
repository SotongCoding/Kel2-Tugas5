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
        [SerializeField] private List<GameObject> _pooledBouncingBullet;
        [SerializeField] private List<GameObject> _pooledBomb;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private GameObject _bouncingBullet;
        [SerializeField] private GameObject _bomb;
        [SerializeField] private int _bulletAmountToPool;
        [SerializeField] private int _bouncingBulletAmountToPool;
        [SerializeField] private int _bombAmountToPool;
        private Bullet _bulletScript;
        private BouncingBullet _bouncingBulletScript;
        [SerializeField] private GameObject _spawnerPointPlayer1;
        [SerializeField] private GameObject _spawnerPointPlayer2;

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<SpawnBulletMessage>(SpawnBullet);
            PublishSubscribe.Instance.Subscribe<MessageSpawnBouncingBullet>(SpawnBouncingBullet);
            PublishSubscribe.Instance.Subscribe<SpawnBombMessage>(SpawnBomb);
        }
        private void OnDestroy()
        {
            PublishSubscribe.Instance.Unsubscribe<SpawnBulletMessage>(SpawnBullet);
            PublishSubscribe.Instance.Unsubscribe<MessageSpawnBouncingBullet>(SpawnBouncingBullet);
            PublishSubscribe.Instance.Unsubscribe<SpawnBombMessage>(SpawnBomb);
        }

        private void Start()
        {
            _pooledBullet = new List<GameObject>();
            _pooledBouncingBullet = new List<GameObject>();
            _pooledBomb = new List<GameObject>();
            _bullet = Resources.Load<GameObject>(@"Prefabs/Bullet");
            _bouncingBullet = Resources.Load<GameObject>(@"Prefabs/BouncingBullet");
            _bomb = Resources.Load<GameObject>(@"Prefabs/Bomb");
            _bulletAmountToPool = 10;
            _bouncingBulletAmountToPool = 10;
            _bombAmountToPool = 10;
            _spawnerPointPlayer1 = GameObject.FindGameObjectWithTag("SpawnPoint1");
            _spawnerPointPlayer2 = GameObject.FindGameObjectWithTag("SpawnPoint2");

            for (int i = 0; i < _bulletAmountToPool; i++)
            {
                GameObject obj = Instantiate(_bullet, transform.position, _bullet.transform.rotation);
                _bulletScript = obj.AddComponent<Bullet>();
                obj.SetActive(false);
                _pooledBullet.Add(obj);
            }

            for (int i = 0; i < _bouncingBulletAmountToPool; i++)
            {
                GameObject obj = Instantiate(_bouncingBullet, transform.position, transform.rotation);
                _bouncingBulletScript = obj.AddComponent<BouncingBullet>();
                obj.SetActive(false);
                _pooledBouncingBullet.Add(obj);
            }

            for (int i = 0; i < _bombAmountToPool; i++)
            {
                GameObject obj = Instantiate(_bomb, transform.position, Quaternion.identity);
                obj.SetActive(false);
                _pooledBomb.Add(obj);
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

        private GameObject GetPooledBouncingBullet()
        {
            for (int i = 0; i < _bouncingBulletAmountToPool; i++)
            {
                if (!_pooledBouncingBullet[i].activeInHierarchy)
                {
                    return _pooledBouncingBullet[i];
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
            GameObject _bulletToSpawn = GetPooledBullet();
            if (message.shooter == "Player1")
            {
                _bulletToSpawn.transform.SetPositionAndRotation(_spawnerPointPlayer1.transform.position, _spawnerPointPlayer1.transform.rotation);
            }
            else if (message.shooter == "Player2")
            {
                _bulletToSpawn.transform.SetPositionAndRotation(_spawnerPointPlayer2.transform.position, _spawnerPointPlayer2.transform.rotation);
            }
            _bulletToSpawn.SetActive(true);
        }

        private void SpawnBouncingBullet(MessageSpawnBouncingBullet message)
        {
            GameObject _bouncingBulletToSpawn = GetPooledBouncingBullet();
            if (message.shooter == "Player1")
            {
                _bouncingBulletToSpawn.transform.SetPositionAndRotation(_spawnerPointPlayer1.transform.position, _spawnerPointPlayer1.transform.rotation);
            }
            else if (message.shooter == "Player2")
            {
                _bouncingBulletToSpawn.transform.SetPositionAndRotation(_spawnerPointPlayer2.transform.position, _spawnerPointPlayer2.transform.rotation);
            }
            _bouncingBulletToSpawn.SetActive(true);
        }

        private void SpawnBomb(SpawnBombMessage message)
        {
                GameObject _bombToSpawn = GetPooledBomb();
                _bombToSpawn.SetActive(true);
        }
    }
}