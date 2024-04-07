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
    public bool underConstruction;
    [HideInInspector] public float moneyToReceive;
    [HideInInspector] public float moneyToReceiveAfterUpgrade;
    private float _moneyMade;
    private float _timer;


    [Header("   Completed Construction Details")]
    public GameObject completedBuilding;
    private CompletedBuilding cBuilding;
    public GameObject appendices;
    
    
    [Header("   Stages")] public List<ConstructionStages> stage;

    private GameManager gameManager;
    // Start is called before the first frame update
    
    void Start()
    {
        EstimateRevenueToReceive();
        Debug.Log("Money to receive: " + moneyToReceive);
        Debug.Log("Money to receive after Upgrade: " + moneyToReceiveAfterUpgrade);
        Debug.Log("remaining time:" + remainingTime);
        remainingTime = buildingDetails.duration;
        gameManager = FindObjectOfType<GameManager>();
        buildingDetails.upgradeLVL = 1;
        cBuilding = completedBuilding.GetComponent<CompletedBuilding>();

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
            
        }

        if (remainingTime <= 0f)
        {
            gameManager.AddMoneyInstant(buildingDetails.completionBonus);
            gameManager.incomePerSecond -= buildingDetails.incomeDuringConstruction;
            gameManager.incomePerSecond += cBuilding.completeBuildingDetails.incomeOverTime;
            underConstruction = false;
            remainingTime = 0f;
            completedBuilding.SetActive(true);
            gameObject.SetActive(false);
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
}

[Serializable]
public struct BuildingDetails
{
    public string buildingName;
    public float duration;
    public int startingCapital;
    public float incomeDuringConstruction;
    public float incomeMultiplier;
    public int upgradeIncomePrice;
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
