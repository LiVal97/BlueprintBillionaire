using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CanvasManager : MonoBehaviour
{
    [Header("   Building Under Construction Details PopUp")]
    public BuildingUnderConstruction buildingUnderConstructionClicked;
    public GameObject notStartedBuildingPopUp;
    public GameObject startConstructionButton;
    public GameObject upgradeButton;
    public GameObject EstimatedRevenueAfterUpgradePannel;
    public GameObject currentEstimatedRevenuePannel;
    public GameObject completionBonusPannel;
    public TMP_Text buildingName;
    public TMP_Text startingCapitalText;
    public TMP_Text upgradePriceText;
    public TMP_Text buildingDurationTime;
    public TMP_Text revenueDuringCompletion;
    public TMP_Text completionBonus;
    public TMP_Text currentEstimatedRevenue;
    public TMP_Text estimatedRevenueAfterUpgrade;
    public TMP_Text buildingLvl;

    [Header("   Completed Building Details PopUp")]
    public CompletedBuilding completedBuildingClicked;
    public GameObject completedBuildingPopUp;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        BUCUpdatedStats();
        BuildingClick();
        CheckBuildingStatus();
    }

    private void BuildingUnderConstructionPopUp()
    {
        notStartedBuildingPopUp.SetActive(true);
        if (buildingUnderConstructionClicked.underConstruction)
        {
            startConstructionButton.SetActive(false);
            upgradeButton.SetActive(true);
            EstimatedRevenueAfterUpgradePannel.SetActive(true);
            currentEstimatedRevenuePannel.SetActive(true);
            completionBonusPannel.SetActive(false);
        }
        if (!buildingUnderConstructionClicked.underConstruction)
        {
            startConstructionButton.SetActive(true);
            upgradeButton.SetActive(false);
            EstimatedRevenueAfterUpgradePannel.SetActive(false);
            currentEstimatedRevenuePannel.SetActive(false);
            completionBonusPannel.SetActive(true);
        }
        
        buildingName.text = buildingUnderConstructionClicked.buildingDetails.buildingName;
        startingCapitalText.text = buildingUnderConstructionClicked.buildingDetails.startingCapital.ToString("#,###");
        upgradePriceText.text = buildingUnderConstructionClicked.buildingDetails.upgradeIncomePrice.ToString("#,###");
        completionBonus.text = buildingUnderConstructionClicked.buildingDetails.completionBonus.ToString("#,###");
    }

    private void BUCUpdatedStats()
    {
        if (buildingUnderConstructionClicked)
        {
            buildingDurationTime.text = buildingUnderConstructionClicked.remainingTime.ToString("#,###");
            revenueDuringCompletion.text =
                buildingUnderConstructionClicked.buildingDetails.incomeDuringConstruction.ToString("#,###.##");
            currentEstimatedRevenue.text = buildingUnderConstructionClicked.moneyToReceive.ToString("#,###");
            estimatedRevenueAfterUpgrade.text =
                buildingUnderConstructionClicked.moneyToReceiveAfterUpgrade.ToString("#,###");
            buildingLvl.text = "LVL: " + buildingUnderConstructionClicked.buildingDetails.upgradeLVL.ToString("#,###");
        }
    }

    private void CheckBuildingStatus()
    {
        if (buildingUnderConstructionClicked != null && !buildingUnderConstructionClicked.gameObject.activeInHierarchy)
        {
            Debug.Log("Constructions Finished");
            notStartedBuildingPopUp.SetActive(false);
        }
    }

    private void BuildingClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask6 = 1 << 6;
            int layerMask7 = 1 << 7;
            RaycastHit rayHit;

            if (Physics.Raycast(ray, out rayHit,100.0f, layerMask6 | layerMask7))
            {
                if (rayHit.transform.gameObject.GetComponent<BuildingUnderConstruction>() != null)
                {
                    buildingUnderConstructionClicked = rayHit.transform.gameObject.GetComponent<BuildingUnderConstruction>();
                    BuildingUnderConstructionPopUp();
                    Debug.Log("Construction " + buildingUnderConstructionClicked.name+" start = " + buildingUnderConstructionClicked.underConstruction);
                }
                if (rayHit.transform.gameObject.GetComponent<CompletedBuilding>() != null)
                {
                    completedBuildingClicked = rayHit.transform.gameObject.GetComponent<CompletedBuilding>();
                    CompletedBuildingPopUp();
                    Debug.Log("CompletedBuildingClicked");
                }
            }
        } 
    }
    
    private void CompletedBuildingPopUp()
    {
        
    }

    public void CloseBuildingUCPopUp()
    {
        notStartedBuildingPopUp.SetActive(false);
        buildingUnderConstructionClicked = null;
    }

    public void UpgradeInProgressBuilding()
    {
        buildingUnderConstructionClicked.BuildingUpgrade();
        upgradePriceText.text = buildingUnderConstructionClicked.buildingDetails.upgradeIncomePrice.ToString("#,###");
        gameManager.incomePerSecond += buildingUnderConstructionClicked.buildingDetails.incomeDuringConstruction - buildingUnderConstructionClicked.buildingDetails.incomeDuringConstruction/buildingUnderConstructionClicked.buildingDetails.incomeMultiplier;
    }

    public void StartConstruction()
    {
        buildingUnderConstructionClicked.underConstruction = true;
        BuildingUnderConstructionPopUp();
        gameManager.incomePerSecond += buildingUnderConstructionClicked.buildingDetails.incomeDuringConstruction;
    }
}
