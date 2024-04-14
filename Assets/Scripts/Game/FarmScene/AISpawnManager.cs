using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;


public class AISpawnManager : MonoBehaviour
{
    public GameObject[] SpawnCars;
    private int randomLeftCar;
    private int randomRightCar;

    public GameObject leftSpawnPosition;
    public GameObject rightSpawnPosition;

    private float randomWaitingTime = 30f;

    private void Awake()
    {
        SpawnCars = Resources.LoadAll<GameObject>("Farm/Vehicles");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LeftSpawnCars());
        StartCoroutine(RightSpawnCars());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator LeftSpawnCars()
    {
        while (true)
        {
            randomLeftCar = Random.Range(0, SpawnCars.Length);
            Instantiate(SpawnCars[randomLeftCar],leftSpawnPosition.transform.position, leftSpawnPosition.transform.rotation, leftSpawnPosition.transform);
            yield return new WaitForSeconds(Random.Range(10, randomWaitingTime));
        }
        
        
    }
    
    private IEnumerator RightSpawnCars()
    {
        while (true)
        {
            randomRightCar = Random.Range(0, SpawnCars.Length);
            Instantiate(SpawnCars[randomRightCar], rightSpawnPosition.transform.position, rightSpawnPosition.transform.rotation, rightSpawnPosition.transform);
            yield return new WaitForSeconds(Random.Range(15, randomWaitingTime));
        }
        
    }
    
}
