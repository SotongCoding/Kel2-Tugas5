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
        public List<PlayerMilestone> savedPlayerMilestone { private set; get; } = new List<PlayerMilestone>();

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

        public void ConvertMileStoneToJSON(int playerId, int mileStoneGet)
        {
            string keyMilestoneHistory = "milestone";
            int index = (savedPlayerMilestone.FindIndex(x => x.playerId == playerId));
            if (index < 0)
                savedPlayerMilestone.Add(new PlayerMilestone(playerId, mileStoneGet));
            else
            {
                if (savedPlayerMilestone[index].milestoneReach < mileStoneGet)
                    savedPlayerMilestone[index] = new PlayerMilestone(playerId, mileStoneGet);
            }


            string playersMilestone = JsonConvert.SerializeObject(savedPlayerMilestone);
            savedData[keyMilestoneHistory] = playersMilestone;
            SaveRecord();
        }

        private Dictionary<string, float> GetAudioData()
        {
            return JsonConvert.DeserializeObject<Dictionary<string, float>>(savedData["audio"]);
        }
        private List<MatchData> GetMatchData()
        {
            return JsonConvert.DeserializeObject<List<MatchData>>(savedData["match"]);
        }
        private List<PlayerMilestone> GetMilestoneData()
        {
            return JsonConvert.DeserializeObject<List<PlayerMilestone>>(savedData["milestone"]);
        }

        public void LoadRecord()
        {
            string gameRecordData = File.ReadAllText(Application.dataPath + "/GameRecord.json");
            savedData = JsonConvert.DeserializeObject<Dictionary<string, string>>(gameRecordData);

            savedAudioData = GetAudioData();
            savedMatchData = GetMatchData();
            savedPlayerMilestone = GetMilestoneData();

            //string gameSetting = File.ReadAllText(Application.dataPath + "/GameSetting.json");
            //savedData = JsonConvert.DeserializeObject<Dictionary<string, float>>(gameSetting);

            //// Check if savedData contain settings from json
            //foreach (KeyValuePair<string, float> pair in savedAudioData) { Debug.Log(pair); }
        }

        private void CreateRecord()
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

        private void SaveRecord()
        {
            string gameRecordData = JsonConvert.SerializeObject(savedData);
            File.WriteAllText(Application.dataPath + "/GameRecord.json", gameRecordData);
        }
        
         public void CalculateMilestone(PlayerMatchRecord matchRecord)
        {  //New Version Calculate

            try
            {
                int level = 0;
                int exp = 0;
                int milestoneGet = 0;

                exp += matchRecord.win * 100;
                exp += matchRecord.lose * 50;

                level = Mathf.RoundToInt(exp / 500);
                milestoneGet = Mathf.RoundToInt(level / 2);

                ConvertMileStoneToJSON(matchRecord.playerId, Mathf.RoundToInt(milestoneGet));
            }
            catch
            {
                ConvertMileStoneToJSON(matchRecord.playerId, Mathf.RoundToInt(matchRecord.win / 3));
            }

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
    public struct PlayerMilestone
    {
        public int playerId;
        public int milestoneReach;

        public PlayerMilestone(int playerId, int milestoneReach)
        {
            this.playerId = playerId;
            this.milestoneReach = milestoneReach;
        }
    }
}