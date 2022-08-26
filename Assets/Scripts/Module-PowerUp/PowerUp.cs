using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agate.MVC.Core;
using TankU.Unit;
using TankU.PubSub;

namespace TankU.PowerUp
{
    public class PowerUp : MonoBehaviour
    {
        [SerializeField]
        private GameObject PowerUps;
        // Start is called before the first frame update
        void Start()
        {
            PowerUps = this.gameObject;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                Debug.Log("Walled!");
                this.gameObject.transform.SetPositionAndRotation(new Vector3(Random.Range(-17.5f, 17.6f), 0.3f, Random.Range(-4, 13)), Quaternion.identity);
            }
            else if (collision.gameObject.CompareTag("Player"))
            {
                if (PowerUps == this.gameObject.CompareTag("BouncePowerUp"))
                {
                    collision.gameObject.GetComponent<Unit.Unit>().BouncingBullet(10);
                    Debug.Log("Bounce");
                }
                else if (PowerUps == this.gameObject.CompareTag("HealthPowerUp"))
                {
                    collision.gameObject.GetComponent<Unit.Unit>().AddHealth();
                    
                }
                this.gameObject.SetActive(false);
            }
        }
    }
}

