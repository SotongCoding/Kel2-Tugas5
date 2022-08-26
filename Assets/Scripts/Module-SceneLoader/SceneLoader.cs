using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace TankU
{
    public class SceneLoader
    {
        private static SceneLoader _instance;
        public static SceneLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SceneLoader();
                }
                return _instance;
            }
        }
        public void LoadScene(string sceneName)
        {
            var targetScene = SceneManager.GetSceneByName(sceneName);
            if (targetScene.isLoaded)
            {
                SceneManager.UnloadSceneAsync(targetScene).completed +=
                delegate { SceneManager.LoadScene(targetScene.buildIndex, LoadSceneMode.Additive); };
                SceneManager.SetActiveScene(targetScene);
            }
            else
            {
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed +=
                delegate { SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName)); };
            }
        }
    }
}