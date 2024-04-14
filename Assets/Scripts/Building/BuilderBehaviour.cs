using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class BuilderBehaviour : MonoBehaviour
{
    public BuilderWaypoint[] wayPoints;

    private Animator anim;
    [SerializeField] private bool isWalking = true;
    [SerializeField] private bool isCarrying;
    [SerializeField] private bool isBuilding;
    [SerializeField] private bool isHitGround;
    [SerializeField] private bool isIdle;
    [SerializeField] private bool isPuttingDown;
    [SerializeField] private bool moveToNextWayP = true;
    
    public int wayPointNo = 0;

    public float timer;
    public float buildingTime;
    public float idleTime;
    public float distanceToWayP;

    public NavMeshAgent builderAgent;

    private GameObject carryObject;
    public GameObject carryingPlace;
    public GameObject hammer;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToWayP = Mathf.Abs(Vector3.Distance(wayPoints[wayPointNo].transform.position, transform.position));
        timer += Time.deltaTime;

        if (moveToNextWayP)
        {
            Debug.Log("Move to next WayP");
            builderAgent.SetDestination(wayPoints[wayPointNo].transform.position);
            if (wayPoints[wayPointNo].isOcupied || !wayPoints[wayPointNo].gameObject.activeInHierarchy)
            {
                wayPointNo++;
            }
            if (wayPointNo > wayPoints.Length -1)
            {
                wayPointNo = 0;
            }
        }
        
        

        /*if (MathF.Abs(wayPoints[wayPointNo].transform.position.z - transform.position.z) <= 0.01f && MathF.Abs(wayPoints[wayPointNo].transform.position.x - transform.position.x) <= 0.01f)
        {
            wayPointNo++;

            if (wayPointNo > wayPoints.Length -1)
            {
                wayPointNo = 0;
            }
        }*/

        if (isIdle)
        {
            if (timer >= idleTime)
            {
                Debug.Log("Stop Idle");
                isIdle = false;
                wayPoints[wayPointNo].isOcupied = false;
                wayPointNo++;
                if (wayPointNo > wayPoints.Length -1)
                {
                    wayPointNo = 0;
                }
                moveToNextWayP = true;
                isWalking = true;
            }
        }
        
        if (isBuilding)
        {
            Vector3 eulerRotation = new Vector3(transform.eulerAngles.x,
                wayPoints[wayPointNo].transform.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Euler(eulerRotation);
            if (timer >= buildingTime)
            {
                Debug.Log("Stop Building");
                isBuilding = false;
                anim.SetBool("isBuilding", isBuilding);
                hammer.SetActive(false);
                wayPoints[wayPointNo].isOcupied = false;
                wayPointNo++;
                if (wayPointNo > wayPoints.Length -1)
                {
                    wayPointNo = 0;
                }
                moveToNextWayP = true;
                isWalking = true;
                anim.SetBool("isWalking", isWalking);
            }
        }
        
        if (isHitGround)
        {
            Vector3 eulerRotation = new Vector3(transform.eulerAngles.x,
                wayPoints[wayPointNo].transform.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Euler(eulerRotation);
            if (timer >= buildingTime)
            {
                Debug.Log("Stop Building");
                isHitGround = false;
                anim.SetBool("isHitingGround", isHitGround);
                hammer.SetActive(false);
                wayPoints[wayPointNo].isOcupied = false;
                wayPointNo++;
                if (wayPointNo > wayPoints.Length -1)
                {
                    wayPointNo = 0;
                }
                moveToNextWayP = true;
                isWalking = true;
                anim.SetBool("isWalking", isWalking);
            }
        }
        
        ChooseAnimation();
    }
    private void ChooseAnimation()
    {
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isCarrying", isCarrying);
        anim.SetBool("isBuilding", isBuilding);
        anim.SetBool("isHitingGround", isHitGround);
        anim.SetBool("isIdle", isIdle);
        anim.SetBool("isPuttingDown", isPuttingDown);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WalkWayP"))
        {
            if (wayPoints[wayPointNo].gameObject.CompareTag("WalkWayP") && distanceToWayP <=2 && moveToNextWayP)
            {
                if (carryObject != null)
                {
                    Destroy(carryObject);
                }
                moveToNextWayP = true;
                isWalking = true;
                isBuilding = false;
                isHitGround = false;
                isCarrying = false;
                isIdle = false;
                isPuttingDown = false;
                anim.SetBool("isWalking", isWalking);
                anim.SetBool("isCarrying", isCarrying);
                anim.SetBool("isBuilding", isBuilding);
                anim.SetBool("isHitingGround", isHitGround);
                anim.SetBool("isIdle", isIdle);
                anim.SetBool("isPuttingDown", isPuttingDown);
                wayPointNo++;
                //Debug.Log("Have reach " + other.gameObject.name);
            }

        }

        if (other.gameObject.CompareTag("CarryWayP"))
        {
            if (wayPoints[wayPointNo].gameObject.CompareTag("CarryWayP")  && distanceToWayP <=2 && moveToNextWayP)
            {
                if (carryObject != null)
                {
                    Destroy(carryObject);
                }
                moveToNextWayP = true;
                isWalking = false;
                isBuilding = false;
                isHitGround = false;
                isCarrying = true;
                isIdle = false;
                isPuttingDown = false;
                anim.SetBool("isWalking", isWalking);
                anim.SetBool("isCarrying", isCarrying);
                anim.SetBool("isBuilding", isBuilding);
                anim.SetBool("isHitingGround", isHitGround);
                anim.SetBool("isIdle", isIdle);
                anim.SetBool("isPuttingDown", isPuttingDown);
                carryObject = Instantiate(wayPoints[wayPointNo].carryObject,carryingPlace.transform.position, wayPoints[wayPointNo].carryObject.transform.rotation, carryingPlace.transform);
                wayPointNo++;
                //Debug.Log("Have reach " + other.gameObject.name);
            }

        }
        
        if (other.gameObject.CompareTag("IdleWayP"))
        {
            if (wayPoints[wayPointNo].gameObject.CompareTag("IdleWayP") && distanceToWayP <=2 && moveToNextWayP)
            {
                anim.SetInteger("IdleIndex", UnityEngine.Random.Range(0, 3));
                if (carryObject != null)
                {
                    Destroy(carryObject);
                }
                timer = 0f;
                moveToNextWayP = false;
                isWalking = false;
                isBuilding = false;
                isHitGround = false;
                isCarrying = false;
                isIdle = true;
                isPuttingDown = false;
                anim.SetBool("isWalking", isWalking);
                anim.SetBool("isCarrying", isCarrying);
                anim.SetBool("isBuilding", isBuilding);
                anim.SetBool("isHitingGround", isHitGround);
                anim.SetBool("isIdle", isIdle);
                anim.SetBool("isPuttingDown", isPuttingDown);
                wayPoints[wayPointNo].isOcupied = true;
                //Debug.Log("Have reach " + other.gameObject.name);
            }
        }
        
        if (other.CompareTag("BuildWayP"))
        {
            if (wayPoints[wayPointNo].gameObject.CompareTag("BuildWayP") && distanceToWayP <= 2 && moveToNextWayP)
            {
                if (carryObject != null)
                {
                    Destroy(carryObject);
                }
                Debug.Log(" Build Collision Detected");
                timer = 0f;
                hammer.SetActive(true);
                moveToNextWayP = false;
                isWalking = false;
                isBuilding = true;
                isHitGround = false;
                isCarrying = false;
                isIdle = false;
                isPuttingDown = false;
                anim.SetBool("isWalking", isWalking);
                anim.SetBool("isCarrying", isCarrying);
                anim.SetBool("isBuilding", isBuilding);
                anim.SetBool("isHitingGround", isHitGround);
                anim.SetBool("isIdle", isIdle);
                anim.SetBool("isPuttingDown", isPuttingDown);
                wayPoints[wayPointNo].isOcupied = true;
                
                //Debug.Log("Have reach " + other.gameObject.name);
            }

        }
        
        if (other.CompareTag("hitGroundWayP"))
        {
            if (wayPoints[wayPointNo].gameObject.CompareTag("hitGroundWayP") && distanceToWayP <= 2 && moveToNextWayP)
            {
                if (carryObject != null)
                {
                    Destroy(carryObject);
                }
                Debug.Log(" Build Collision Detected");
                timer = 0f;
                hammer.SetActive(true);
                moveToNextWayP = false;
                isWalking = false;
                isBuilding = false;
                isHitGround = true;
                isCarrying = false;
                isIdle = false;
                isPuttingDown = false;
                anim.SetBool("isWalking", isWalking);
                anim.SetBool("isCarrying", isCarrying);
                anim.SetBool("isBuilding", isBuilding);
                anim.SetBool("isHitingGround", isHitGround);
                anim.SetBool("isIdle", isIdle);
                anim.SetBool("isPuttingDown", isPuttingDown);
                wayPoints[wayPointNo].isOcupied = true;
                
                //Debug.Log("Have reach " + other.gameObject.name);
            }

        }
        
        if (other.CompareTag("PlaceOnTheGroundWayP"))
        {
            if (wayPoints[wayPointNo].gameObject.CompareTag("PlaceOnTheGroundWayP") && distanceToWayP <=2 && moveToNextWayP)
            {
                moveToNextWayP = false;
                isWalking = false;
                isBuilding = false;
                isHitGround = false;
                isCarrying = false;
                isIdle = false;
                isPuttingDown = true;
                anim.SetBool("isWalking", isWalking);
                anim.SetBool("isCarrying", isCarrying);
                anim.SetBool("isBuilding", isBuilding);
                anim.SetBool("isHitingGround", isHitGround);
                anim.SetBool("isIdle", isIdle);
                anim.SetBool("isPuttingDown", isPuttingDown);
                //Debug.Log("Have reach " + other.gameObject.name);
            }

        }
    }

    public void FinishPlacingOnGroundAnim()
    {
        moveToNextWayP = true;
        isWalking = true;
        isBuilding = false;
        isHitGround = false;
        isCarrying = false;
        isIdle = false;
        isPuttingDown = false;
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isCarrying", isCarrying);
        anim.SetBool("isBuilding", isBuilding);
        anim.SetBool("isHitingGround", isHitGround);
        anim.SetBool("isIdle", isIdle);
        anim.SetBool("isPuttingDown", isPuttingDown);
        wayPointNo++;
    }

    public void DestroyCarriedObjAnimEvent()
    {
        Destroy(carryObject);
    }
}
