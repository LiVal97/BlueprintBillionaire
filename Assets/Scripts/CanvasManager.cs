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
    
    private BuildingUnderConstruction buildingUnderConstructionClicked;
    [Header("   Building Under Construction Details PopUp")]
    public GameObject bucNotStartedBuildingPopUp;
    public GameObject bucStartConstructionButton;
    public GameObject bucUpgradeButton;
    public GameObject bucEstimatedRevenueAfterUpgradePannel;
    public GameObject bucCurrentEstimatedRevenuePannel;
    public GameObject bucCompletionBonusPannel;
    public TMP_Text bucBuildingNameText;
    public TMP_Text bucStartingCapitalText;
    public TMP_Text bucUpgradePriceText;
    public TMP_Text bucBuildingDurationTime;
    public TMP_Text bucRevenueDuringCompletion;
    public TMP_Text bucCompletionBonus;
    public TMP_Text bucCurrentEstimatedRevenue;
    public TMP_Text bucEstimatedRevenueAfterUpgrade;
    public TMP_Text bucBuildingLvl;

    [Header("   Completed Building Details PopUp")]
    public CompletedBuilding completedBuildingClicked;
    public GameObject completedBuildingPopUp;
    public TMP_Text cbNameText;
    public TMP_Text cbRevenuePerSecondAmountText;
    public TMP_Text cbLvlText;
    public TMP_Text cbUpgradePriceText;

    [Header("   Workers")] 
    public GameObject addWorkers;
    public GameObject addWorkerButton;
    public GameObject exitWorkerShopButton;
    public TMP_Text hireAmountToPayText;
    public TMP_Text workerNoText;
    
    [Header("   Money")]
    public TMP_Text moneyText;
    public TMP_Text incomePerSecondText;

    [Header("   Settings")] 
    public GameObject settingsPanel;
    public Slider musicSlider;
    public Slider soundSlider;
    private bool _settingsIsOpen;
    

    private GameManager gameManager;
    private AudioManager _audioManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        UpdateHireAmountToPay();
        BucUpdatedStats();
        BuildingClick();
        CheckBuildingStatus();
        PresentMoney();
        PresentWorkersNo();
    }

    private void BuildingUnderConstructionPopUp()
    {
        bucNotStartedBuildingPopUp.SetActive(true);
        if (buildingUnderConstructionClicked.underConstruction)
        {
            bucStartConstructionButton.SetActive(false);
            bucUpgradeButton.SetActive(true);
            bucEstimatedRevenueAfterUpgradePannel.SetActive(true);
            bucCurrentEstimatedRevenuePannel.SetActive(true);
            bucCompletionBonusPannel.SetActive(false);
        }
        if (!buildingUnderConstructionClicked.underConstruction)
        {
            bucStartConstructionButton.SetActive(true);
            bucUpgradeButton.SetActive(false);
            bucEstimatedRevenueAfterUpgradePannel.SetActive(false);
            bucCurrentEstimatedRevenuePannel.SetActive(false);
            bucCompletionBonusPannel.SetActive(true);
        }
        
        bucBuildingNameText.text = buildingUnderConstructionClicked.buildingDetails.buildingName;
        bucStartingCapitalText.text = buildingUnderConstructionClicked.buildingDetails.startingCapital.ToString("#,###");
        bucRevenueDuringCompletion.text =
            buildingUnderConstructionClicked.buildingDetails.incomeDuringConstruction.ToString("#,###.##");
        bucUpgradePriceText.text = buildingUnderConstructionClicked.buildingDetails.upgradeIncomePrice.ToString("#,###");
        bucCompletionBonus.text = buildingUnderConstructionClicked.buildingDetails.completionBonus.ToString("#,###");
    }

    private void BucUpdatedStats()
    {
        if (buildingUnderConstructionClicked)
        {
            bucBuildingDurationTime.text = buildingUnderConstructionClicked.remainingTime.ToString("#,###");
            bucCurrentEstimatedRevenue.text = buildingUnderConstructionClicked.moneyToReceive.ToString("#,###");
            bucEstimatedRevenueAfterUpgrade.text =
                buildingUnderConstructionClicked.moneyToReceiveAfterUpgrade.ToString("#,###");
            bucBuildingLvl.text = "LVL: " + buildingUnderConstructionClicked.buildingDetails.upgradeLVL.ToString("#,###");
        }
    }
    
    

    private void CheckBuildingStatus()
    {
        if (buildingUnderConstructionClicked != null && !buildingUnderConstructionClicked.gameObject.activeInHierarchy)
        {
            Debug.Log("Constructions Finished");
            bucNotStartedBuildingPopUp.SetActive(false);
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
                    CloseCbPopUp();
                }
                if (rayHit.transform.gameObject.GetComponent<CompletedBuilding>() != null)
                {
                    completedBuildingClicked = rayHit.transform.gameObject.GetComponent<CompletedBuilding>();
                    CompletedBuildingPopUp();
                    Debug.Log("CompletedBuildingClicked");
                    CloseBuildingUCPopUp();
                }
            }
        } 
    }
    
    private void CompletedBuildingPopUp()
    {
        completedBuildingPopUp.SetActive(true);
        cbNameText.text = completedBuildingClicked.completeBuildingDetails.buildingName;
        cbRevenuePerSecondAmountText.text =
            completedBuildingClicked.completeBuildingDetails.incomeOverTime.ToString("#,###.##");
        cbUpgradePriceText.text = completedBuildingClicked.completeBuildingDetails.upgradeIncomePrice.ToString("#,###");
    }

    public void CloseBuildingUCPopUp()
    {
        bucNotStartedBuildingPopUp.SetActive(false);
        buildingUnderConstructionClicked = null;
    }

    public void CloseCbPopUp()
    {
        completedBuildingPopUp.SetActive(false);
        completedBuildingClicked = null;
    }

    public void UpgradeInProgressBuilding()
    {
        if (buildingUnderConstructionClicked.buildingDetails.upgradeIncomePrice <= gameManager.money)
        {
            gameManager.RemoveMoney(buildingUnderConstructionClicked.buildingDetails.upgradeIncomePrice);
            buildingUnderConstructionClicked.BuildingUpgrade();
            bucRevenueDuringCompletion.text =
                buildingUnderConstructionClicked.buildingDetails.incomeDuringConstruction.ToString("#,###.##");
            bucUpgradePriceText.text = buildingUnderConstructionClicked.buildingDetails.upgradeIncomePrice.ToString("#,###");
            gameManager.incomePerSecond += buildingUnderConstructionClicked.buildingDetails.incomeDuringConstruction - buildingUnderConstructionClicked.buildingDetails.incomeDuringConstruction/buildingUnderConstructionClicked.buildingDetails.incomeMultiplier;
            //Update building info in the save file
            buildingUnderConstructionClicked.UpdateBipDetails();
        }
    }

    public void UpgradeCompletedBuilding()
    {
        if (completedBuildingClicked.completeBuildingDetails.upgradeIncomePrice <= gameManager.money)
        {
            gameManager.RemoveMoney(completedBuildingClicked.completeBuildingDetails.upgradeIncomePrice);
            completedBuildingClicked.UpgradeCompleteBuilding();
            cbUpgradePriceText.text = completedBuildingClicked.completeBuildingDetails.upgradeIncomePrice.ToString("#,###");
            cbRevenuePerSecondAmountText.text =
                completedBuildingClicked.completeBuildingDetails.incomeOverTime.ToString("#,###.##");
            gameManager.incomePerSecond += completedBuildingClicked.completeBuildingDetails.incomeOverTime -
                                           completedBuildingClicked.completeBuildingDetails.incomeOverTime / completedBuildingClicked
                                               .completeBuildingDetails.incomeMultiplier;
            //Update building info in the save file
            completedBuildingClicked.UpdateCbDetails();
        }
        
    }

    public void StartConstruction()
    {
        if (buildingUnderConstructionClicked.buildingDetails.startingCapital <= gameManager.money)
        {
            gameManager.RemoveMoney(buildingUnderConstructionClicked.buildingDetails.startingCapital);
            buildingUnderConstructionClicked.underConstruction = true;
            BuildingUnderConstructionPopUp();
            gameManager.incomePerSecond += buildingUnderConstructionClicked.buildingDetails.incomeDuringConstruction;
            //Save building info in the save file
            buildingUnderConstructionClicked.SaveBip();
        }
        
    }

    public void AddWorkers()
    {
        _audioManager.ClickSound();
        addWorkers.SetActive(true);
        addWorkerButton.SetActive(false);
        exitWorkerShopButton.SetActive(true);
    }

    public void GiveUpAddWorkers()
    {
        _audioManager.ClickSound();
        addWorkers.SetActive(false);
        addWorkerButton.SetActive(true);
        exitWorkerShopButton.SetActive(false);
    }

    public void HireWorkers()
    {
        _audioManager.ClickSound();
        gameManager.HireWorker();
        
    }

    public void UpdateHireAmountToPay()
    {
        if (addWorkers.gameObject.activeInHierarchy)
        {
            hireAmountToPayText.text = gameManager.hirePrice.ToString("#,###");
        }
    }

    private void PresentMoney()
    {
        if (gameManager.money == 0)
        {
            moneyText.text = "0";
        }
        
        if (gameManager.money > 0 )
        {
            moneyText.text = gameManager.money.ToString("#,###");
        }
        
        if (gameManager.incomePerSecond == 0)
        {
            incomePerSecondText.text = "0/Sec";
        }
        
        if (gameManager.incomePerSecond > 0)
        {
            incomePerSecondText.text = gameManager.incomePerSecond.ToString("#,###.##") + "/Sec";    
        }
    }

    private void PresentWorkersNo()
    {
        workerNoText.text = gameManager.workersNo.ToString("#,###");
    }

    public void OpenSettings()
    {
        _audioManager.ClickSound();
        if (!_settingsIsOpen)
        {
            settingsPanel.SetActive(!_settingsIsOpen);
            _settingsIsOpen = !_settingsIsOpen;
        }
        else
        {
            settingsPanel.SetActive(!_settingsIsOpen);
            _settingsIsOpen = !_settingsIsOpen;
        }
        
    }
}
