using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agate.MVC.Core;
using TankU.PubSub;

namespace TankU.MainMenu
{
    public class MainMenuUi : MonoBehaviour
    {
        [SerializeField]
        private Canvas MainMenuCanvas;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GameplayScene()
        {
            SceneLoader.Instance.LoadScene("Gameplay");
            MainMenuCanvas.gameObject.SetActive(false);
        }
    }
}

