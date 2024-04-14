using System;
using System.Collections;
using System.Collections.Generic;
using SuperPivot;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BuildingUnderConstruction : MonoBehaviour
{
    [Header("   Construction Site Details")]
    public BuildingDetails buildingDetails;
    [HideInInspector] public float remainingTime;
    [HideInInspector] public float moneyToReceive;
    [HideInInspector] public float moneyToReceiveAfterUpgrade;
    private float _moneyMade;
    private float _timer;
    public bool underConstruction;
    public GameObject builders;

    [Header("   Completed Construction Details")]
    public GameObject completedBuilding;
    private CompletedBuilding cBuilding;
    public GameObject appendices;
    
    [Header("   Stages")] 
    public List<ConstructionStages> stage;

    private GameManager gameManager;
    private GlobalManager _globalManager;

    void Start()
    {
        EstimateRevenueToReceive();
        /*Debug.Log("Money to receive: " + moneyToReceive);
        Debug.Log("Money to receive after Upgrade: " + moneyToReceiveAfterUpgrade);
        Debug.Log("remaining time:" + remainingTime);*/
        remainingTime = buildingDetails.duration;
        gameManager = FindObjectOfType<GameManager>();
        _globalManager = FindObjectOfType<GlobalManager>();
        buildingDetails.upgradeLVL = 1;
        cBuilding = completedBuilding.GetComponent<CompletedBuilding>();
        ActivateCb();
        LoadBipData();
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(SaveBipUpdatesRecurrent());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (underConstruction)
        {
            remainingTime -= Time.deltaTime;
            //gameManager.AddMoneyOverTime(buildingDetails.incomeDuringConstruction);
            EstimateRevenueToReceive();
            Debug.Log(gameObject.name + "Start construction");
            builders.SetActive(true);
        }

        if (remainingTime <= 0f)
        {
            gameManager.AddMoneyInstant(buildingDetails.completionBonus);
            gameManager.incomePerSecond -= buildingDetails.incomeDuringConstruction;
            //gameManager.incomePerSecond += cBuilding.completeBuildingDetails.incomeOverTime;
            underConstruction = false;
            remainingTime = 0f;
            completedBuilding.SetActive(true);
            StopCoroutine(SaveBipUpdatesRecurrent());
            gameObject.SetActive(false);
            UpdateBipDetails();
            SaveData.SaveCurrentData(_globalManager.playersData);
        }
        
        //Activate gameObject of each stage at the time goes by
        for (int i = 0; i < stage.Count; i++)
        {
            if (buildingDetails.duration - remainingTime >= buildingDetails.duration / (stage.Count +1) * (i+1))
            {
                for (int j = 0; j < stage[i].buildingPartToAppear.Length; j++)
                {
                    stage[i].buildingPartToAppear[j].SetActive(true);
                }
                
                for (int j = 0; j < stage[i].buildingPartToDisappear.Length; j++)
                {
                    stage[i].buildingPartToDisappear[j].SetActive(false);
                }
            }
        }
        
    }

    //Add on upgrade button in order to upgrade the building
    public void BuildingUpgrade()
    {
        buildingDetails.incomeDuringConstruction *= buildingDetails.incomeMultiplier;
        buildingDetails.upgradeIncomePrice = (int) (buildingDetails.upgradeIncomePrice * buildingDetails.upgradePriceMultiplier);
        buildingDetails.upgradeLVL++;
    }

    //Show the player the remaining amount of money to receive at current building LVL and at next
    private void EstimateRevenueToReceive()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1f)
        {
            _timer = 0f;
            _moneyMade += buildingDetails.incomeDuringConstruction;
        }
        moneyToReceive = (int) _moneyMade + remainingTime * buildingDetails.incomeDuringConstruction;
        moneyToReceiveAfterUpgrade = (int)_moneyMade + remainingTime * buildingDetails.incomeDuringConstruction *
                                     buildingDetails.incomeMultiplier - buildingDetails.upgradeIncomePrice;
    }

    public void SaveBip()
    {
        _globalManager.playersData.buildingInProgressStatsList.Add(new BuildingInProgressStats(buildingDetails.buildingName, gameObject.activeInHierarchy, underConstruction, remainingTime, buildingDetails.incomeDuringConstruction, buildingDetails.upgradeIncomePrice, buildingDetails.upgradeLVL));
        
    }

    public void UpdateBipDetails()
    {
        for (int i = 0; i < _globalManager.playersData.buildingInProgressStatsList.Count; i++)
        {
            if (_globalManager.playersData.buildingInProgressStatsList[i].bipName == buildingDetails.buildingName)
            {
                _globalManager.playersData.buildingInProgressStatsList[i].bipStatus = gameObject.activeInHierarchy;
                _globalManager.playersData.buildingInProgressStatsList[i].bipInConstructionStatus = underConstruction;
                _globalManager.playersData.buildingInProgressStatsList[i].bipRemainingTime = remainingTime;
                _globalManager.playersData.buildingInProgressStatsList[i].bipIncomeDuringConstruction =
                    buildingDetails.incomeDuringConstruction;
                _globalManager.playersData.buildingInProgressStatsList[i].bipUpgradeIncomePrice =
                    buildingDetails.upgradeIncomePrice;
                _globalManager.playersData.buildingInProgressStatsList[i].bipLvl = buildingDetails.upgradeLVL;
            }
        }
        
    }

    private void LoadBipData()
    {
        for (int i = 0; i < _globalManager.playersData.buildingInProgressStatsList.Count; i++)
        {
            if (_globalManager.playersData.buildingInProgressStatsList[i].bipName == buildingDetails.buildingName)
            {
                gameObject.SetActive(_globalManager.playersData.buildingInProgressStatsList[i].bipStatus);
                underConstruction = _globalManager.playersData.buildingInProgressStatsList[i].bipInConstructionStatus;
                remainingTime = _globalManager.playersData.buildingInProgressStatsList[i].bipRemainingTime;
                buildingDetails.incomeDuringConstruction = _globalManager.playersData.buildingInProgressStatsList[i].bipIncomeDuringConstruction;
                buildingDetails.upgradeIncomePrice = _globalManager.playersData.buildingInProgressStatsList[i].bipUpgradeIncomePrice;
                buildingDetails.upgradeLVL = _globalManager.playersData.buildingInProgressStatsList[i].bipLvl;
            }
        }
    }

    private IEnumerator SaveBipUpdatesRecurrent()
    {
        while (true)
        {
            UpdateBipDetails();
            yield return new WaitForSeconds(2);
        }
    }

    private void ActivateCb()
    {
        for (int i = 0; i < _globalManager.playersData.completedBuildingsStatsList.Count; i++)
        {
            if (_globalManager.playersData.completedBuildingsStatsList[i].cbName == cBuilding.completeBuildingDetails.buildingName)
            {
                if (_globalManager.playersData.completedBuildingsStatsList[i].cbSaved)
                {
                    completedBuilding.SetActive(true);
                }
                
            }

        }
    }
}

[Serializable]
public struct BuildingDetails
{
    public string buildingName;
    public float duration;
    public int startingCapital;
    public float incomeDuringConstruction;
    public float incomeMultiplier;
    public float upgradeIncomePrice;
    public float upgradePriceMultiplier;
    public float completionBonus;
    public int upgradeLVL;
}

[Serializable]
public struct ConstructionStages
{
    public GameObject[] buildingPartToAppear;
    public GameObject[] buildingPartToDisappear;
}
