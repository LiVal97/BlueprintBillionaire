using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public PlayersData playersData;

    private void Awake()
    {
        //ResetSaveData();
        playersData = SaveData.LoadData();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetSaveData()
    {
        playersData.musicVolume = 1f;
        playersData.effectsVolume = 1f;
        playersData.money = 2000f;
        playersData.revenuePerSecond = 0f;
        playersData.workers = 1;
        //playersData.hireWorkerPrice = 100f;
        playersData.completedBuildingsStatsList.Clear();
        playersData.buildingInProgressStatsList.Clear();
        SaveData.SaveCurrentData(playersData);
    }
    
}
