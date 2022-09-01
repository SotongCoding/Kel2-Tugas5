using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Agate.MVC.Core;
using TankU.PubSub;

namespace TankU.PowerUpSpawner
{
    public class PowerUpSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject HealthPowerUp, BouncePowerUp;
        [SerializeField]
        private float SpawnTime, PowerUpTime;
        private int RngPowerUp;
        private bool isSpawn = false;
        private bool isGameStart = false;

        // Start is called before the first frame update
        void Start()
        {
            PublishSubscribe.Instance.Subscribe<PubSub.MessageStartGameplay>(ReceiveMessageStartGameplay);
            PublishSubscribe.Instance.Subscribe<PubSub.MessageTieBreaker>(ReceiveMessageTieBreak);

            BouncePowerUp = GameObject.Find("Bounce-Crate");
            HealthPowerUp = GameObject.Find("Health-Crate");
            //Set Spawing Time 
            SpawnTime = 2;
            //---------------

            BouncePowerUp.SetActive(false);
            HealthPowerUp.SetActive(false);

        }

        private void OnDisable()
        {
            PublishSubscribe.Instance.Unsubscribe<PubSub.MessageStartGameplay>(ReceiveMessageStartGameplay);
            PublishSubscribe.Instance.Unsubscribe<PubSub.MessageTieBreaker>(ReceiveMessageTieBreak);
        }

        // Update is called once per frame
        void Update()
        {
            if (isGameStart)
            {
                PowerUpSpawning();
            }
            PowerUpLasting();

        }

        private void PowerUpSpawning()
        {
            if (SpawnTime >= 0 && isSpawn == false)
            {
                SpawnTime -= Time.deltaTime;
            }
            else if (SpawnTime <= 0 && isSpawn == false)
            {
                RngPowerUp = Random.Range(0, 3);
                if (RngPowerUp == 0 || RngPowerUp == 2)
                {
                    HealthPowerUp.SetActive(true);
                    HealthPowerUp.transform.SetPositionAndRotation(new Vector3(Random.Range(-18.5f, 13.5f), 0.3f, Random.Range(2, -21)), Quaternion.identity);
                    //reset Spawn Time
                    SpawnTime = 2;
                    //--------------
                    PowerUpTime = 10;
                    isSpawn = true;
                }
                else if (RngPowerUp == 1 || RngPowerUp == 3)
                {
                    BouncePowerUp.SetActive(true);
                    BouncePowerUp.transform.SetPositionAndRotation(new Vector3(Random.Range(-18.5f, 13.5f), 0.3f, Random.Range(2, -21)), Quaternion.identity);
                    //reset Spawn Time
                    SpawnTime = 2;
                    //----------------
                    PowerUpTime = 10;
                    isSpawn = true;
                }
            }
        }
        private void PowerUpLasting()
        {
            if (isSpawn == true)
            {
                PowerUpTime -= Time.deltaTime;
            }

            if (PowerUpTime <= 0)
            {
                isSpawn = false;
                BouncePowerUp.SetActive(false);
                HealthPowerUp.SetActive(false);
            }
        }
        private void ReceiveMessageStartGameplay(PubSub.MessageStartGameplay message)
        {
            isGameStart = true;
        }
        private void ReceiveMessageTieBreak(MessageTieBreaker obj)
        {
            isGameStart = false;
        }
    }
}

