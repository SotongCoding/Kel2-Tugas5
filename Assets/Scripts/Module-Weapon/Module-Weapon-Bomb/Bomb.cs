using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using TankU.PubSub;
using UnityEngine;

namespace TankU.Weapon.Bomb
{
    public class Bomb : Weapon
    {
        private bool _activatedOnce;
        private float _detonateWaitDuration;
        private float _explosionRadius;
        //[SerializeField] private List<GameObject> _explosionHitObjects = new List<GameObject>();

        private void OnEnable()
        {
            _activatedOnce = true;
            if (_activatedOnce)
            {
                StartCoroutine(Detonate(_detonateWaitDuration));
            }
        }

        private void Awake()
        {
            _detonateWaitDuration = 3f;
            _explosionRadius = 3f;
        }

        IEnumerator Detonate(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            RaycastHit[] raycastHits = Physics.SphereCastAll(gameObject.transform.position, _explosionRadius, Vector3.one, Mathf.Infinity);
            foreach (RaycastHit hit in raycastHits)
            {
                //// Check which objects get hit
                //_explosionHitObjects.Add(hit.transform.gameObject);

                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    PublishSubscribe.Instance.Publish<Hit>(new Hit(damagePoint));
                }
            }
            gameObject.SetActive(false);
        }

        //void OnDrawGizmosSelected()
        //{
        //    // View explosion radius for debug purposes
        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawSphere(transform.position, _explosionRadius);
        //}
    }
}