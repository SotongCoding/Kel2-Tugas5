using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public static class PlayerPrefsImprove
{
    public static void SetDictionary<TKey, TValue>(string key, Dictionary<TKey, TValue> dictionary)
    {
        List<DictionaryWarpData<TKey, TValue>> listOfData = new List<DictionaryWarpData<TKey, TValue>>();

        foreach (var item in dictionary)
        {
            listOfData.Add(new DictionaryWarpData<TKey, TValue>(item.Key, item.Value));
        }

        PlayerPrefs.SetString(key, JsonUtility.ToJson(new DictionarySaveData<TKey, TValue>(listOfData)));
    }
    public static Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string key)
    {

        DictionarySaveData<TKey, TValue> saveData =
        JsonUtility.FromJson<DictionarySaveData<TKey, TValue>>(PlayerPrefs.GetString(key));

        Dictionary<TKey, TValue> rawDictionary = new Dictionary<TKey, TValue>();

        List<DictionaryWarpData<TKey, TValue>> listOfData = new List<DictionaryWarpData<TKey, TValue>>();

        foreach (var item in saveData.data)
        {
            rawDictionary.Add(item.key, item.value);
        }

        return rawDictionary;

    }

    public static void SetStructObject<TObject>(string key, TObject obj) where TObject : struct
    {
        string JSON = JsonUtility.ToJson(obj);
        PlayerPrefs.SetString(key, JSON);

    }
    public static TObject SetStructObject<TObject>(string key) where TObject : struct
    {
        return JsonUtility.FromJson<TObject>(PlayerPrefs.GetString(key));
    }

    [System.Serializable]
    public struct DictionarySaveData<TKey, TValue>
    {
        public List<DictionaryWarpData<TKey, TValue>> data;

        public DictionarySaveData(List<DictionaryWarpData<TKey, TValue>> data)
        {
            this.data = data;
        }
    }

    [System.Serializable]
    public struct DictionaryWarpData<TKey, TValue>
    {
        public TKey key;
        public TValue value;

        public DictionaryWarpData(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }
}
