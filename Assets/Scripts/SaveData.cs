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
    public float cbIncomeOverTime;
    public float cbUpgradeIncomePrice;
    public int cbLvl;

    public CompletedBuildingsStats(string cbName, bool cbStatus, float cbIncomeOverTime, float cbUpgradeIncomePrice, int cbLvl)
    {
        this.cbName = cbName;
        this.cbStatus = cbStatus;
        this.cbIncomeOverTime = cbIncomeOverTime;
        this.cbUpgradeIncomePrice = cbUpgradeIncomePrice;
        this.cbLvl = cbLvl;
    }
}