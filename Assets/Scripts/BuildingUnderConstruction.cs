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
        underConstruction = true;
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
                for (int j = 0; j < stage[i].buildingPart.Length; j++)
                {
                    stage[i].buildingPart[j].SetActive(true);
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
    public int incomeDuringConstruction;
}

[Serializable]

public struct ConstructionStages
{
    public GameObject[] buildingPart;
}
