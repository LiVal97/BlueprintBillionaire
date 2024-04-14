using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public PlayersData playersData;
    public PlayersData currentData;

    private void Awake()
    {
        //ResetSaveData();
        currentData = SaveData.LoadData();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        playersData = currentData;
        SaveData.SaveCurrentData(playersData);
    }

    private void ResetSaveData()
    {
        playersData.musicVolume = 0.3f;
        playersData.effectsVolume = 1f;
        playersData.money = 2000f;
        playersData.revenuePerSecond = 0f;
        playersData.totalWorkers = 1;
        playersData.availableWorkers = 1;
        //playersData.hireWorkerPrice = 100f;
        playersData.completedBuildingsStatsList.Clear();
        playersData.buildingInProgressStatsList.Clear();
        SaveData.SaveCurrentData(playersData);
    }
    
}
