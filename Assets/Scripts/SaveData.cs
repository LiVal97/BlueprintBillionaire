using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    private static readonly string FilePath = Application.persistentDataPath + "/saveData.json";
    private GlobalManager _globalManager;

    private void Awake()
    {
        _globalManager = FindObjectOfType<GlobalManager>();
    }

    public static void SaveCurrentData(PlayersData data)
    {
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(FilePath, jsonData);
    }

    public static PlayersData LoadData()
    {
        if (File.Exists(FilePath))
        {
            string jsonData = File.ReadAllText(FilePath);
            return JsonUtility.FromJson<PlayersData>(jsonData);
        }
        else
        {
            PlayersData newPlayer = new PlayersData();
            
            newPlayer.musicVolume = 0.3f;
            newPlayer.effectsVolume = 1f;
            newPlayer.money = 2000f;
            newPlayer.revenuePerSecond = 0f;
            newPlayer.totalWorkers = 1;
            newPlayer.availableWorkers = 1;
            //newPlayer.hireWorkerPrice = 100f;
            return newPlayer;
        }
    }
}

[Serializable]

public class PlayersData
{
    public float musicVolume;
    public float effectsVolume;
    public float money;
    public float revenuePerSecond;
    public int totalWorkers;
    public int availableWorkers;

    public List<BuildingInProgressStats> buildingInProgressStatsList = new List<BuildingInProgressStats>();
    public List<CompletedBuildingsStats> completedBuildingsStatsList = new List<CompletedBuildingsStats>();
}

[Serializable]
public class BuildingInProgressStats
{
    public string bipName;
    public bool bipStatus;
    public bool bipInConstructionStatus;
    public float bipRemainingTime;
    public float bipIncomeDuringConstruction;
    public float bipUpgradeIncomePrice;
    public int bipLvl;

    public BuildingInProgressStats(string bipName, bool bipStatus, bool bipInConstructionStatus, 
        float bipRemainingTime, float bipIncomeDuringConstruction, float bipUpgradeIncomePrice, int bipLvl)
    {
        this.bipName = bipName;
        this.bipStatus = bipStatus;
        this.bipInConstructionStatus = bipInConstructionStatus;
        this.bipRemainingTime = bipRemainingTime;
        this.bipIncomeDuringConstruction = bipIncomeDuringConstruction;
        this.bipUpgradeIncomePrice = bipUpgradeIncomePrice;
        this.bipLvl = bipLvl;
    }
}

[Serializable]
public class CompletedBuildingsStats
{
    public string cbName;
    public bool cbStatus;
    public bool cbSaved;
    public float cbIncomeOverTime;
    public float cbUpgradeIncomePrice;
    public int cbLvl;

    public CompletedBuildingsStats(string cbName, bool cbStatus, bool cbSaved, float cbIncomeOverTime, float cbUpgradeIncomePrice, int cbLvl)
    {
        this.cbName = cbName;
        this.cbStatus = cbStatus;
        this.cbSaved = cbSaved;
        this.cbIncomeOverTime = cbIncomeOverTime;
        this.cbUpgradeIncomePrice = cbUpgradeIncomePrice;
        this.cbLvl = cbLvl;
    }
}