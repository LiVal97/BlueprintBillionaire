using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]

public class PlayersData
{
    public float musicVolume;
    public float effectsVolume;
    public float money;
    public float revenuePerSecond;
    public int workers;

    //public List<String> buildingInProgressName;
    //public List<String> completedBuildingsName;
    public List<BuildingInProgressStats> BuildingInProgressStatsList;
    public List<CompletedBuildingsStats> CompletedBuildingsStatsList;
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
}

[Serializable]
public class CompletedBuildingsStats
{
    public string cbName;
    public bool cbStatus;
    public float cbIncomeOverTime;
    public float cbUpgradeIncomePrice;
    public int cbLvl;
}