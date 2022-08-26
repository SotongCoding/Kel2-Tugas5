using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agate.MVC.Core;

namespace TankU.GameplayUI
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] BombP1, BombP2, BombP3, BombP4;
        private int[] BombLeft;
        // Start is called before the first frame update
        void Start()
        {
            BombLeft = new int[4];
            BombLeft[0] = 5;
            BombLeft[1] = 5;
            BombLeft[2] = 5;
            BombLeft[3] = 5;
        }

        private void Awake()
        {
            PublishSubscribe.Instance.Subscribe<MessageSpawnBomb>(ReduceBomb);
        }
        // Update is called once per frame
        void Update()
        {
            
        }

        void ReduceBomb(MessageSpawnBomb message)
        {
            if (message.PlayerId == 1)
            {
                BombLeft[0]--;
                BombP1[BombLeft[0]].SetActive(false);
            }
            else if (message.PlayerId == 2)
            {
                BombLeft[1]--;
                BombP2[BombLeft[1]].SetActive(false);
            }
            else if (message.PlayerId == 3)
            {
                BombLeft[2]--;
                BombP2[BombLeft[2]].SetActive(false);
            }
            else if (message.PlayerId == 4)
            {
                BombLeft[3]--;
                BombP2[BombLeft[3]].SetActive(false);
            }
        }
        
    }
}

