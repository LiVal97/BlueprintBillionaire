using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletedBuilding : MonoBehaviour
{
    public CompleteBuildingDetails completeBuildingDetails;

    private GameManager _gameManager;
    private GlobalManager _globalManager;

    public bool buildingCompleted;
    
    // Start is called before the first frame update
    void Start()
    {
        buildingCompleted = true;
        _gameManager = FindObjectOfType<GameManager>();
        _globalManager = FindObjectOfType<GlobalManager>();
        SaveCb();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeCompleteBuilding()
    {
        completeBuildingDetails.incomeOverTime *= completeBuildingDetails.incomeMultiplier;
        completeBuildingDetails.upgradeIncomePrice = (int) (completeBuildingDetails.upgradeIncomePrice *
                                                     completeBuildingDetails.upgradePriceMultiplier);
        completeBuildingDetails.upgradeLVL++;
    }

    public void SaveCb()
    {
        _globalManager.playersData.completedBuildingsStatsList.Add(new CompletedBuildingsStats(completeBuildingDetails.buildingName, gameObject.activeInHierarchy, completeBuildingDetails.incomeOverTime, completeBuildingDetails.upgradeIncomePrice, completeBuildingDetails.upgradeLVL));
    }

    public void UpdateCbDetails()
    {
        for (int i = 0; i < _globalManager.playersData.completedBuildingsStatsList.Count; i++)
        {
            if (_globalManager.playersData.completedBuildingsStatsList[i].cbName == completeBuildingDetails.buildingName)
            {
                _globalManager.playersData.completedBuildingsStatsList[i].cbStatus = gameObject.activeInHierarchy;
                _globalManager.playersData.completedBuildingsStatsList[i].cbIncomeOverTime =
                    completeBuildingDetails.incomeOverTime;
                _globalManager.playersData.completedBuildingsStatsList[i].cbUpgradeIncomePrice =
                    completeBuildingDetails.upgradeIncomePrice;
                _globalManager.playersData.completedBuildingsStatsList[i].cbLvl = completeBuildingDetails.upgradeLVL;
            }
        }
    }
}

[Serializable]
public struct CompleteBuildingDetails
{
    public string buildingName;
    public float incomeOverTime;
    public float incomeMultiplier;
    public int upgradeIncomePrice;
    public float upgradePriceMultiplier;
    public int upgradeLVL;
}
