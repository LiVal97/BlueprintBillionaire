using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AISpawnManager : MonoBehaviour
{
    public GameObject[] leftSideSpawnCars;
    private int randomLeftCar;
    public GameObject[] rightSideSpawnCars;
    private int randomRightCar;

    public GameObject leftSpawnPosition;
    public GameObject rightSpawnPosition;

    private float randomWaitingTime = 20f;
    
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
        randomLeftCar = Random.Range(0, leftSideSpawnCars.Length);
        Instantiate(leftSideSpawnCars[randomLeftCar],leftSpawnPosition.transform.position, leftSideSpawnCars[randomLeftCar].transform.rotation);
        yield return new WaitForSeconds(Random.Range(0, randomWaitingTime));
    }
    
    private IEnumerator RightSpawnCars()
    {
        randomRightCar = Random.Range(0, rightSideSpawnCars.Length);
        Instantiate(rightSideSpawnCars[randomRightCar], rightSpawnPosition.transform.position, rightSideSpawnCars[randomRightCar].transform.rotation);
        yield return new WaitForSeconds(Random.Range(0, randomWaitingTime));
    }
    
}
