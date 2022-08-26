using Agate.MVC.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.Weapon.Bomb
{
    public class Bomb : Weapon
    {
        private bool _activatedOnce;
        private float _detonateWaitDuration;

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
        }

        IEnumerator Detonate(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            PublishSubscribe.Instance.Publish<MessageSpawnBullet>(new MessageSpawnBullet(gameObject.transform, gameObject.transform, true));
            for (int i = 0; i < 4; i++)
            {   
                switch (i)
                {
                    case 1:
                        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 90, transform.eulerAngles.z);
                        PublishSubscribe.Instance.Publish<MessageSpawnBullet>(new MessageSpawnBullet(gameObject.transform, gameObject.transform, true));
                        break;
                    case 2:
                        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 180, transform.eulerAngles.z);
                        PublishSubscribe.Instance.Publish<MessageSpawnBullet>(new MessageSpawnBullet(gameObject.transform, gameObject.transform, true));
                        break;
                    case 3:
                        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 270, transform.eulerAngles.z);
                        PublishSubscribe.Instance.Publish<MessageSpawnBullet>(new MessageSpawnBullet(gameObject.transform, gameObject.transform, true));
                        break;
                }
            }
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0, transform.eulerAngles.z);
            gameObject.SetActive(false);
        }
    }
}