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

    public GameObject[] appendicesLvl25;
    public GameObject[] appendicesLvl50;
    public GameObject[] appendicesLvl75;
    public GameObject[] appendicesLvl100;
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
        
        //StartCoroutine(SaveCbUpdatesRecurrent());
    }

    private void Update()
    {
        ShowAppendices();
    }

    private void ShowAppendices()
    {
        
        if (completeBuildingDetails.upgradeLVL >= 10)
        {
            for (int i = 0; i < appendicesLvl25.Length; i++)
            {
                appendicesLvl25[i].SetActive(true);
            }
        }
        if (completeBuildingDetails.upgradeLVL >= 20)
        {
            for (int i = 0; i < appendicesLvl50.Length; i++)
            {
                appendicesLvl50[i].SetActive(true);
            }
        }
        if (completeBuildingDetails.upgradeLVL >= 30)
        {
            for (int i = 0; i < appendicesLvl75.Length; i++)
            {
                appendicesLvl75[i].SetActive(true);
            }
        }
        if (completeBuildingDetails.upgradeLVL >= 40)
        {
            for (int i = 0; i < appendicesLvl100.Length; i++)
            {
                appendicesLvl100[i].SetActive(true);
            }
        }
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
        _globalManager.currentData.completedBuildingsStatsList.Add(new CompletedBuildingsStats(completeBuildingDetails.buildingName, gameObject.activeInHierarchy,_cbSaved, completeBuildingDetails.incomeOverTime, completeBuildingDetails.upgradeIncomePrice, completeBuildingDetails.upgradeLVL));
        _globalManager.currentData.revenuePerSecond += completeBuildingDetails.incomeOverTime;
        _globalManager.SaveGame();
    }

    public void UpdateCbData()
    {
        for (int i = 0; i < _globalManager.currentData.completedBuildingsStatsList.Count; i++)
        {
            if (_globalManager.currentData.completedBuildingsStatsList[i].cbName == completeBuildingDetails.buildingName)
            {
                _globalManager.currentData.completedBuildingsStatsList[i].cbStatus = gameObject.activeInHierarchy;
                _globalManager.currentData.completedBuildingsStatsList[i].cbIncomeOverTime =
                    completeBuildingDetails.incomeOverTime;
                _globalManager.currentData.completedBuildingsStatsList[i].cbUpgradeIncomePrice =
                    completeBuildingDetails.upgradeIncomePrice;
                _globalManager.currentData.completedBuildingsStatsList[i].cbLvl = completeBuildingDetails.upgradeLVL;
            }
        }
    }
    
    public void LoadCbData()
    {
        for (int i = 0; i < _globalManager.currentData.completedBuildingsStatsList.Count; i++)
        {
            if (_globalManager.currentData.completedBuildingsStatsList[i].cbName == completeBuildingDetails.buildingName)
            {
                    gameObject.SetActive(_globalManager.currentData.completedBuildingsStatsList[i].cbStatus);
                    completeBuildingDetails.incomeOverTime = _globalManager.currentData.completedBuildingsStatsList[i].cbIncomeOverTime;
                    completeBuildingDetails.upgradeIncomePrice = _globalManager.currentData.completedBuildingsStatsList[i].cbUpgradeIncomePrice;
                    completeBuildingDetails.upgradeLVL = _globalManager.currentData.completedBuildingsStatsList[i].cbLvl;
                    _cbSaved = _globalManager.currentData.completedBuildingsStatsList[i].cbSaved;
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
    public float upgradeIncomePrice;
    public float upgradePriceMultiplier;
    public int upgradeLVL;
}
