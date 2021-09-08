using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    public static DataHandler STATS;
    public DataHandler_SaveData _SaveData = new DataHandler_SaveData();

    void Start()
    {
        STATS = this;

        LoadData();
    }

    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(_SaveData, true);
        File.WriteAllText(Application.persistentDataPath + "/PlaytestData.json", jsonData);
    }
    public void LoadData()
    {
        try
        {
            string dataAsJson = File.ReadAllText(Application.persistentDataPath + "/PlaytestData.json");
            _SaveData = JsonUtility.FromJson<DataHandler_SaveData>(dataAsJson);
        }
        catch
        {
            SaveData();
        }
    }
    public DataHandler_SaveData GetSaveData()
    {
        return _SaveData;
    }


    public void CreateNewSave()
    {
        DataHandler_Data newsave = new DataHandler_Data();
        _SaveData.saveData.Add(newsave);
    }
}

[System.Serializable]
public class DataHandler_SaveData
{
    public List <DataHandler_Data> saveData = new List<DataHandler_Data>();
}
[System.Serializable]
public class DataHandler_Data
{
    public string SettingsID;
    public float TimePlayed;
    public int DebrisCollected;
    public int RocketsLaunched;
    public int RocketCollisions;
}
