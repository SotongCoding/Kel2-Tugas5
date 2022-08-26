using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankU.GameSetting
{
    public class GameSetting : MonoBehaviour
    {
        #region Singleton

        public static GameSetting Instance;

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        [SerializeField] Dictionary<string, object> saveData = new Dictionary<string, object>();

        public void ConvertToJSON()
        {
            JsonUtility.ToJson(new SaveData());
            Debug.Log("bikin json");
            Debug.Log(saveData);
        }

        private void Start()
        {
            Debug.Log(saveData);
        }


        public void SaveData(string name, object value)
        {
            Debug.Log("Masuk");
            saveData[name] = value;
            ConvertToJSON();
        }
    }

    public struct SaveData
    {
        public float soundSFX;
        public float soundBGM;
    }
}