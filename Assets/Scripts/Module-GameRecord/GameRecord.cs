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

        private Dictionary<string, string> savedData = new Dictionary<string, string>();

        public Dictionary<string, float> savedAudioData { private set; get; } = new Dictionary<string, float>();
        public List<MatchData> savedMatchData { private set; get; } = new List<MatchData>();
        public List<int> playerMilestone { private set; get; } = new List<int>();

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

        public void ConvertMatchHistoryToJSON(int playerWin, int[] playerLoses)
        {
            string keyMatchHistory = "match";
            MatchData matchData = new MatchData(playerWin, playerLoses);
            savedMatchData.Add(matchData);

            string matchHistory = JsonConvert.SerializeObject(savedMatchData);
            savedData[keyMatchHistory] = matchHistory;
            SaveRecord();
            //savedMatchData = GetMatchData();
        }

        private Dictionary<string, float> GetAudioData()
        {
            return JsonConvert.DeserializeObject<Dictionary<string, float>>(savedData["audio"]);
        }
        private AudioData GetAudioData_()
        {
            string JSONAUdio = savedData["audio"];

            return JsonUtility.FromJson<AudioData>(JSONAUdio);
        }

        void SetAudio()
        {
            float volume = GetAudioData_().soundBGM;
        }

        private List<MatchData> GetMatchData()
        {
            return JsonConvert.DeserializeObject<List<MatchData>>(savedData["match"]);
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
            List<MatchData> matchDatas = new List<MatchData>();

            string matchHistory = JsonConvert.SerializeObject(matchDatas);
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

    [System.Serializable]
    public struct MatchData
    {
        public int winPlayer;
        public int[] losePlayers;

        public MatchData(int winPlayer, int[] losePlayers)
        {
            this.winPlayer = winPlayer;
            this.losePlayers = losePlayers;
        }
    }
}