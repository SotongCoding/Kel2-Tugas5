using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using TankU.PubSub;
using UnityEngine;

namespace TankU.Bomb
{
    public class Bomb : MonoBehaviour, IPoolObject
    {
        protected bool _activatedOnce;
        protected float _detonateWaitDuration;
        protected float _explosionRadius;
        protected int damagePoint = 1;

        public PoolingSystem poolingSystem { private set; get; }

        private void OnEnable()
        {
            _activatedOnce = true;
            if (_activatedOnce)
            {
                StartCoroutine(Detonate(_detonateWaitDuration));
            }
        }

        protected virtual void Awake()
        {
            _detonateWaitDuration = 3f;
            _explosionRadius = 3f;
        }

        protected IEnumerator Detonate(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            PublishSubscribe.Instance.Publish<MessageSoundfx>(new MessageSoundfx("bomb_explosion"));
            PublishSubscribe.Instance.Publish<MessageVfx>(new MessageVfx("bomb_explosion", this.transform.position));

            RaycastHit[] raycastHits = Physics.SphereCastAll(gameObject.transform.position, _explosionRadius, Vector3.one, Mathf.Infinity);
            foreach (RaycastHit hit in raycastHits)
            {
                var hitTarget = hit.transform.gameObject;
                //// Check which object gets hit
                //Debug.Log(hit.transform.gameObject.name);

                if (hitTarget.CompareTag("Player"))
                {
                    if (hitTarget.TryGetComponent(out IBombHitAble bombHitAble))
                    {
                        hitTarget.GetComponent<Unit.Unit>().ReciveBombDamage();
                    }
                }
            }
            gameObject.SetActive(false);
        }

        //// View explosion radius for debug purposes
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, _explosionRadius);
        }

        void IPoolObject.Initial(PoolingSystem poolSystem)
        {
            Debug.Log("Intial" + poolSystem);
            poolingSystem = poolSystem;
        }

        public void OnCreate()
        {

        }

        public void StoreToPool()
        {
            Debug.Log(poolingSystem);
            poolingSystem.Store(this);
            gameObject.SetActive(false);
        }
    }

    public interface IBombHitAble
    {
        void ReciveBombDamage();
    }
}