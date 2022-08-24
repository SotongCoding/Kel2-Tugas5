using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // Start is called before the first frame update
        void Start()
        {
            BouncePowerUp = GameObject.Find("Bounce-PowerUp");
            HealthPowerUp = GameObject.Find("Health-PowerUp");
            SpawnTime = 10;
            //Instantiate(HealthPowerUp, new Vector3(Random.Range(-8, 8), 0.3f, Random.Range(-2.7f, 7.7f)), Quaternion.identity);
            //Instantiate(BouncePowerUp, new Vector3(Random.Range(-8, 8), 0.3f, Random.Range(-2.7f, 7.7f)), Quaternion.identity);
            BouncePowerUp.SetActive(false);
            HealthPowerUp.SetActive(false);
            
        }
        
        // Update is called once per frame
        void Update()
        {
            PowerUpSpawning();
            PowerUpLasting();
            //Debug.Log(SpawnTime);
            if (isSpawn == true)
            {
                //Debug.Log("RNG: " + RngPowerUp + "Time: " + PowerUpTime);
            }
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
                    HealthPowerUp.transform.SetPositionAndRotation(new Vector3(Random.Range(-8, 8), 0.3f, Random.Range(-2.7f, 7.7f)), Quaternion.identity);
                    SpawnTime = 10;
                    PowerUpTime = 10;
                    isSpawn = true;
                }
                else if (RngPowerUp == 1 || RngPowerUp == 3)
                {
                    BouncePowerUp.SetActive(true);
                    BouncePowerUp.transform.SetPositionAndRotation(new Vector3(Random.Range(-8, 8), 0.3f, Random.Range(-2.7f, 7.7f)), Quaternion.identity);
                    SpawnTime = 10;
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

        

    }
}

