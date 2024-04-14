using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public PlayersData playersData;
    public PlayersData currentData;
    public static GlobalManager instance;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
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
        currentData.musicVolume = 0.3f;
        currentData.effectsVolume = 1f;
        currentData.money = 2000f;
        currentData.revenuePerSecond = 0f;
        currentData.totalWorkers = 1;
        currentData.availableWorkers = 1;
        currentData.completedBuildingsStatsList.Clear();
        currentData.buildingInProgressStatsList.Clear();
        SaveGame();
    }
    
}
