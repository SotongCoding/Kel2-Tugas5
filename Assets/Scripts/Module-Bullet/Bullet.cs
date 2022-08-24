using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.Bullet
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private int _moveSpeed;

        // Start is called before the first frame update
        void Start()
        {
            _moveSpeed = 1;
            
            Rigidbody _rb = gameObject.AddComponent<Rigidbody>();
            _rb.useGravity = false;
            _rb.angularDrag = 0;
        }

        // Update is called once per frame
        void Update()
        {
            MoveBullet();
        }

        private void MoveBullet()
        {
            transform.Translate(_moveSpeed * Time.deltaTime * Vector3.forward, this.transform.parent);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Wall"))
            {
                gameObject.SetActive(false);
            }
            else if (other.CompareTag("Player"))
            {
                PublishSubscribe.Instance.Publish<HitByBullet>(new HitByBullet(1));
            }
        }
    }

    public struct HitByBullet
    {
        int _damage;

        public HitByBullet(int damage)
        {
            this._damage = damage;
        }
    }
}