using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace TankU.GameSetting
{
    public class GameSetting : MonoBehaviour
    {
        public static GameSetting Instance;

        private void Awake()
        {
            Instance = this;
            LoadData();
        }

        public Dictionary<string, float> savedData = new Dictionary<string, float>();

        public void ConvertToJSON(SaveData save)
        {
            string gameSetting = JsonUtility.ToJson(save);
            File.WriteAllText(Application.dataPath + "/GameSetting.json", gameSetting);
        }

        public void LoadData()
        {
            string gameSetting = File.ReadAllText(Application.dataPath + "/GameSetting.json");
            savedData = JsonConvert.DeserializeObject<Dictionary<string, float>>(gameSetting);

            //// Check if savedData contain settings from json
            //foreach (KeyValuePair<string, float> pair in savedData) { Debug.Log(pair); }
        }
    }

    public struct SaveData
    {
        public float soundBGM;
        public float soundSFX;
    }
}