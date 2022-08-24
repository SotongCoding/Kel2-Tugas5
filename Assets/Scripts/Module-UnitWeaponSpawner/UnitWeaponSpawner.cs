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

        void Start()
        {
            _pooledBullet = new List<GameObject>();
            _pooledBomb = new List<GameObject>();
            _bullet = Resources.Load<GameObject>(@"Prefabs/Bullet");
            _bomb = Resources.Load<GameObject>(@"Prefabs/Bomb");
            _bulletAmountToPool = 10;
            _bombAmountToPool = 10;

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

        public GameObject GetPooledBullet()
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

        public GameObject GetPooledBomb()
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
    }
}