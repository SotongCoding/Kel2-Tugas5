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

        private void Start() {
            PublishSubscribe.Instance.Publish<MessageSoundBgm>(new MessageSoundBgm("mainmenu"));
        }

        public void GameplayScene()
        {
            SceneLoader.Instance.LoadScene("Gameplay");
            MainMenuCanvas.gameObject.SetActive(false);
        }
        public void OptionScene()
        {
            OptionUI.OptionUI.Instance.SetUIOption(true);
        }
    }
}
