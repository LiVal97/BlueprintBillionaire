using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
    public TMP_Text bucBuildersNeededText;
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
    public Slider sfxSlider;
    private bool _settingsIsOpen;
    

    private GameManager gameManager;
    private AudioManager _audioManager;
    private GlobalManager _globalManager;

    [SerializeField] private bool popUpActive;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        _audioManager = FindObjectOfType<AudioManager>();
        _globalManager = FindObjectOfType<GlobalManager>();
        RefreshSettings();
    }

    private void Update()
    {
        UpdateHireAmountToPay();
        BucUpdatedStats();
        BuildingClick();
        CheckBipStatus();
        PresentMoney();
        PresentWorkersNo();

        if (!bucNotStartedBuildingPopUp.activeInHierarchy && !completedBuildingPopUp.activeInHierarchy)
        {
            popUpActive = false;
        }
    }

    private void BuildingUnderConstructionPopUp()
    {
        popUpActive = true;
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
        bucStartingCapitalText.text = buildingUnderConstructionClicked.buildingDetails.startingCapital.ToString("#,##0");
        bucBuildersNeededText.text = buildingUnderConstructionClicked.buildingDetails.buildersNeeded.ToString("#,##0");
        bucRevenueDuringCompletion.text =
            buildingUnderConstructionClicked.buildingDetails.incomeDuringConstruction.ToString("#,##0.##");
        bucUpgradePriceText.text = buildingUnderConstructionClicked.buildingDetails.upgradeIncomePrice.ToString("#,##0");
        bucCompletionBonus.text = buildingUnderConstructionClicked.buildingDetails.completionBonus.ToString("#,##0");
        bucBuildingLvl.text = "LVL: " + buildingUnderConstructionClicked.buildingDetails.upgradeLVL.ToString("#,##0");
    }

    private void BucUpdatedStats()
    {
        if (buildingUnderConstructionClicked)
        {
            bucBuildingDurationTime.text = buildingUnderConstructionClicked.remainingTime.ToString("#,##0");
            bucCurrentEstimatedRevenue.text = buildingUnderConstructionClicked.moneyToReceive.ToString("#,##0");
            bucEstimatedRevenueAfterUpgrade.text =
                buildingUnderConstructionClicked.moneyToReceiveAfterUpgrade.ToString("#,##0");
            //bucBuildingLvl.text = "LVL: " + buildingUnderConstructionClicked.buildingDetails.upgradeLVL.ToString("#,###");
        }
    }
    
    private void CheckBipStatus()
    {
        if (buildingUnderConstructionClicked != null && !buildingUnderConstructionClicked.gameObject.activeInHierarchy)
        {
            Debug.Log("Constructions Finished");
            bucNotStartedBuildingPopUp.SetActive(false);
        }
    }

    private void BuildingClick()
    {
        if (!popUpActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                int layerMask6 = 1 << 6;
                int layerMask7 = 1 << 7;
                int layerMask5UI = 1 << 5;
                RaycastHit rayHit;

                if (Physics.Raycast(ray, out rayHit,100.0f, layerMask6 | layerMask7))
                {
                    if (rayHit.transform.gameObject.GetComponent<BuildingUnderConstruction>() != null)
                    {
                        buildingUnderConstructionClicked = rayHit.transform.gameObject.GetComponent<BuildingUnderConstruction>();
                        CloseCbPopUp();
                        BuildingUnderConstructionPopUp();
                        Debug.Log("Construction " + buildingUnderConstructionClicked.name+" start = " + buildingUnderConstructionClicked.underConstruction);
                        
                    }
                    if (rayHit.transform.gameObject.GetComponent<CompletedBuilding>() != null)
                    {
                        completedBuildingClicked = rayHit.transform.gameObject.GetComponent<CompletedBuilding>();
                        CloseBuildingUCPopUp();
                        CompletedBuildingPopUp();
                        Debug.Log("CompletedBuildingClicked");
                        
                    }
                }
            } 
        }
         
    }
    
    private void CompletedBuildingPopUp()
    {
        popUpActive = true;
        completedBuildingPopUp.SetActive(true);
        cbNameText.text = completedBuildingClicked.completeBuildingDetails.buildingName;
        cbRevenuePerSecondAmountText.text =
            completedBuildingClicked.completeBuildingDetails.incomeOverTime.ToString("#,##0.##");
        cbUpgradePriceText.text = completedBuildingClicked.completeBuildingDetails.upgradeIncomePrice.ToString("#,##0");
        cbLvlText.text = "LVL: " + completedBuildingClicked.completeBuildingDetails.upgradeLVL.ToString("#,##0");
    }

    public void CloseBuildingUCPopUp()
    {
        popUpActive = false;
        bucNotStartedBuildingPopUp.SetActive(false);
        buildingUnderConstructionClicked = null;
    }

    public void CloseCbPopUp()
    {
        popUpActive = false;
        completedBuildingPopUp.SetActive(false);
        completedBuildingClicked = null;
    }

    public void UpgradeInProgressBuilding()
    {
        if (buildingUnderConstructionClicked.buildingDetails.upgradeIncomePrice <= _globalManager.currentData.money)
        {
            gameManager.RemoveMoney(buildingUnderConstructionClicked.buildingDetails.upgradeIncomePrice);
            buildingUnderConstructionClicked.BuildingUpgrade();
            bucRevenueDuringCompletion.text =
                buildingUnderConstructionClicked.buildingDetails.incomeDuringConstruction.ToString("#,##0.##");
            bucUpgradePriceText.text = buildingUnderConstructionClicked.buildingDetails.upgradeIncomePrice.ToString("#,##0");
            _globalManager.currentData.revenuePerSecond += buildingUnderConstructionClicked.buildingDetails.incomeDuringConstruction - buildingUnderConstructionClicked.buildingDetails.incomeDuringConstruction/buildingUnderConstructionClicked.buildingDetails.incomeMultiplier;
            bucBuildingLvl.text = "LVL: " + buildingUnderConstructionClicked.buildingDetails.upgradeLVL.ToString("#,##0");
            //Update building info in the save file
            buildingUnderConstructionClicked.UpdateBipDetails();
            _globalManager.SaveGame();
        }
    }

    public void UpgradeCompletedBuilding()
    {
        if (completedBuildingClicked.completeBuildingDetails.upgradeIncomePrice <= _globalManager.currentData.money)
        {
            gameManager.RemoveMoney(completedBuildingClicked.completeBuildingDetails.upgradeIncomePrice);
            
            completedBuildingClicked.UpgradeCompleteBuilding();
            
            cbUpgradePriceText.text = completedBuildingClicked.completeBuildingDetails.upgradeIncomePrice.ToString("#,##0");
            cbRevenuePerSecondAmountText.text =
                completedBuildingClicked.completeBuildingDetails.incomeOverTime.ToString("#,##0.##");
            cbLvlText.text = "LVL: " + completedBuildingClicked.completeBuildingDetails.upgradeLVL.ToString("#,##0");
            
            _globalManager.currentData.revenuePerSecond += completedBuildingClicked.completeBuildingDetails.incomeOverTime -
                                           completedBuildingClicked.completeBuildingDetails.incomeOverTime / completedBuildingClicked
                                               .completeBuildingDetails.incomeMultiplier;
            
            //Update building info in the save file
            completedBuildingClicked.UpdateCbData();
            _globalManager.SaveGame();
        }
        
    }

    public void StartConstruction()
    {
        if (buildingUnderConstructionClicked.buildingDetails.startingCapital <= _globalManager.currentData.money && buildingUnderConstructionClicked.buildingDetails.buildersNeeded <= _globalManager.currentData.availableWorkers)
        {
            gameManager.RemoveMoney(buildingUnderConstructionClicked.buildingDetails.startingCapital);
            _globalManager.currentData.availableWorkers -= buildingUnderConstructionClicked.buildingDetails.buildersNeeded;
            buildingUnderConstructionClicked.underConstruction = true;
            BuildingUnderConstructionPopUp();
            _globalManager.currentData.revenuePerSecond += buildingUnderConstructionClicked.buildingDetails.incomeDuringConstruction;
            //Save building info in the save file
            buildingUnderConstructionClicked.SaveBip();
            _globalManager.SaveGame();
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
        _globalManager.SaveGame();
    }

    private void UpdateHireAmountToPay()
    {
        if (addWorkers.gameObject.activeInHierarchy)
        {
            hireAmountToPayText.text = gameManager.hirePrice.ToString("#,##0");
        }
    }

    private void PresentMoney()
    {
        if (_globalManager.currentData.money >= 0 )
        {
            moneyText.text = _globalManager.currentData.money.ToString("#,##0");
        }
        if (_globalManager.currentData.revenuePerSecond >= 0)
        {
            incomePerSecondText.text = _globalManager.currentData.revenuePerSecond.ToString("#,##0.##") + "/Sec";    
        }
    }

    private void PresentWorkersNo()
    {
        workerNoText.text = _globalManager.currentData.availableWorkers.ToString("#,##0") + " / " +_globalManager.currentData.totalWorkers.ToString("#,##0");
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

    public void ExitGame()
    {
        _globalManager.SaveGame();
        Application.Quit();
    }
    
    private void RefreshSettings()
    {
        musicSlider.value = _globalManager.currentData.musicVolume;
        sfxSlider.value = _globalManager.currentData.effectsVolume;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void ChangeMusicVolume()
    {
        AudioManager.instance.MusicVolume(musicSlider.value);
    }
    
    public void ChangeSfxVolume()
    {
        AudioManager.instance.SfxVolume(sfxSlider.value);
    }
}
