using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankU.GameplayUI
{
    public class UnitStatusUI : MonoBehaviour
    {
        [SerializeField]
        private Unit.Unit Player;
        [SerializeField]
        private GameObject[] Bomb;
        [SerializeField]
        private int BombLeft;
        [SerializeField]
        private Slider HealthBar;

        public void UpdateHealth()
        {
            HealthBar.value = Player._health;
        }
        public void ReduceBomb()
        {
            BombLeft--;
            Bomb[BombLeft].SetActive(false);
        }
    }
}
