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

    private bool _cbSaved;
    
    // Start is called before the first frame update
    void Start()
    {
        buildingCompleted = true;
        _gameManager = FindObjectOfType<GameManager>();
        _globalManager = FindObjectOfType<GlobalManager>();
        
        LoadCbData();
        if (!_cbSaved)
        {
            SaveCb();
        }
        
        StartCoroutine(SaveCbUpdatesRecurrent());
    }

    public void UpgradeCompleteBuilding()
    {
        completeBuildingDetails.incomeOverTime *= completeBuildingDetails.incomeMultiplier;
        completeBuildingDetails.upgradeIncomePrice = (int) (completeBuildingDetails.upgradeIncomePrice *
                                                     completeBuildingDetails.upgradePriceMultiplier);
        completeBuildingDetails.upgradeLVL++;
    }

    private void SaveCb()
    {
        _cbSaved = true;
        _globalManager.playersData.completedBuildingsStatsList.Add(new CompletedBuildingsStats(completeBuildingDetails.buildingName, gameObject.activeInHierarchy,_cbSaved, completeBuildingDetails.incomeOverTime, completeBuildingDetails.upgradeIncomePrice, completeBuildingDetails.upgradeLVL));
        _gameManager.incomePerSecond += completeBuildingDetails.incomeOverTime;
        SaveData.SaveCurrentData(_globalManager.playersData);
    }

    public void UpdateCbData()
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
    
    public void LoadCbData()
    {
        for (int i = 0; i < _globalManager.playersData.completedBuildingsStatsList.Count; i++)
        {
            if (_globalManager.playersData.completedBuildingsStatsList[i].cbName == completeBuildingDetails.buildingName)
            {
                    gameObject.SetActive(_globalManager.playersData.completedBuildingsStatsList[i].cbStatus);
                    completeBuildingDetails.incomeOverTime = _globalManager.playersData.completedBuildingsStatsList[i].cbIncomeOverTime;
                    completeBuildingDetails.upgradeIncomePrice = _globalManager.playersData.completedBuildingsStatsList[i].cbUpgradeIncomePrice;
                    completeBuildingDetails.upgradeLVL = _globalManager.playersData.completedBuildingsStatsList[i].cbLvl;
                    _cbSaved = _globalManager.playersData.completedBuildingsStatsList[i].cbSaved;
            }
        }
    }

    private IEnumerator SaveCbUpdatesRecurrent()
    {
        while (true)
        {
            UpdateCbData();
            yield return new WaitForSeconds(120);
        }
    }
}

[Serializable]
public struct CompleteBuildingDetails
{
    public string buildingName;
    public float incomeOverTime;
    public float incomeMultiplier;
    public float upgradeIncomePrice;
    public float upgradePriceMultiplier;
    public int upgradeLVL;
}
