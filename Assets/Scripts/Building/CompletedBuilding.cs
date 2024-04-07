using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletedBuilding : MonoBehaviour
{
    public CompleteBuildingDetails completeBuildingDetails;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
