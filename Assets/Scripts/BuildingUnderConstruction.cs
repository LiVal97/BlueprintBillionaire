using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUnderConstruction : MonoBehaviour
{
    [Header("   Construction Site Details")]
    public BuildingDetails buildingDetails;
    public float remainingTime;
    public bool underConstruction;

    [Header("   Completed Construction Details")]
    public GameObject completedBuilding;
    public GameObject appendices;
    
    [Header("   Stages")] public List<ConstructionStages> stage;
    // Start is called before the first frame update
    void Start()
    {
        remainingTime = buildingDetails.duration;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (underConstruction)
        {
            remainingTime -= Time.deltaTime;
        }

        if (remainingTime <= 0f)
        {
            underConstruction = false;
            remainingTime = 0f;
            completedBuilding.SetActive(true);
            gameObject.SetActive(false);
        }

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

    private void OnMouseDown()
    {
        Debug.Log(buildingDetails.buildingName);
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
    private float maximumIncome;
    public int upgradeLVL;
}

[Serializable]

public struct ConstructionStages
{
    public GameObject[] buildingPartToAppear;
    public GameObject[] buildingPartToDisappear;
}
