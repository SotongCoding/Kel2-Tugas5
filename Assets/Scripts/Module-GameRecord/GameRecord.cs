using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace TankU.GameRecord
{
    public class GameRecord : MonoBehaviour
    {
        public static GameRecord Instance;

        private void Awake()
        {
            Instance = this;
            
            if (!File.Exists(Application.dataPath + "/GameRecord.json"))
            {
                Debug.Log("NotDetected");
                CreateRecord();
            }
            else
            {
                Debug.Log("Detected");
                LoadRecord();
            }
        }

        public Dictionary<string, string> savedData = new Dictionary<string, string>();
        public Dictionary<string, float> savedAudioData = new Dictionary<string, float>();
        public Dictionary<int, int> savedMatchData = new Dictionary<int, int>();

        public void ConvertAudioToJSON(AudioData save)
        {
            //string gameSetting = JsonUtility.ToJson(save, true);
            //File.WriteAllText(Application.dataPath + "/GameSetting.json", gameSetting);
            //savedData = JsonConvert.DeserializeObject<Dictionary<string, float>>(gameSetting);

            string keySound = "audio";
            string saveSound = JsonUtility.ToJson(save);
            Debug.Log(saveSound);
            savedData[keySound] = saveSound;
            SaveRecord();
            savedAudioData = GetAudioData();
        }

        public void ConvertMatchHistoryToJSON(int playerID)
        {
            string keyMatchHistory = "match";
            savedMatchData[playerID]++;
            string matchHistory = JsonConvert.SerializeObject(savedMatchData);
            savedData[keyMatchHistory] = matchHistory;
            SaveRecord();
            savedMatchData = GetMatchData();
        }

        private Dictionary<string, float> GetAudioData()
        {
            return JsonConvert.DeserializeObject<Dictionary<string, float>>(savedData["audio"]);
        }

        private Dictionary<int, int> GetMatchData()
        {
            return JsonConvert.DeserializeObject<Dictionary<int, int>>(savedData["match"]);
        }

        public void LoadRecord()
        {
            string gameRecordData = File.ReadAllText(Application.dataPath + "/GameRecord.json");
            savedData = JsonConvert.DeserializeObject<Dictionary<string, string>>(gameRecordData);
            
            savedAudioData = GetAudioData();
            savedMatchData = GetMatchData();

            //string gameSetting = File.ReadAllText(Application.dataPath + "/GameSetting.json");
            //savedData = JsonConvert.DeserializeObject<Dictionary<string, float>>(gameSetting);

            //// Check if savedData contain settings from json
            //foreach (KeyValuePair<string, float> pair in savedAudioData) { Debug.Log(pair); }
        }

        public void CreateRecord()
        {
            AudioData _save = new();
            _save.soundBGM = 1f;
            _save.soundSFX = 1f;
            string keySound = "audio";
            string saveSound = JsonUtility.ToJson(_save);
            //File.WriteAllText(Application.dataPath + "/Audio.json", saveSound);
            savedData.Add(keySound, saveSound);
            //============================================================

            string keyMatchHistory = "match";
            Dictionary<int, int> scorePlayer = new Dictionary<int, int>(){
                {1,0},
                {2,0},
                {3,0},
                {4,0},
            };
            string matchHistory = JsonConvert.SerializeObject(scorePlayer);
            //File.WriteAllText(Application.dataPath + "/MatchHistory.json", matchHistory);
            savedData.Add(keyMatchHistory, matchHistory);

            string gameRecordData = JsonConvert.SerializeObject(savedData);
            File.WriteAllText(Application.dataPath + "/GameRecord.json", gameRecordData);
            savedAudioData = GetAudioData();
            savedMatchData = GetMatchData();
        }

        public void SaveRecord()
        {
            string gameRecordData = JsonConvert.SerializeObject(savedData);
            File.WriteAllText(Application.dataPath + "/GameRecord.json", gameRecordData);
        }
    }

    public struct AudioData
    {
        public float soundBGM;
        public float soundSFX;
    }
}