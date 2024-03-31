using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public Transform[] wayPoints;
    
    public GameObject rightWheel;
    public GameObject leftWheel;
    public GameObject wayPointsParent;

    public float rotationSpeed;
    public float movementSpeed;
    public int firstWayPoint;
    private int wayPointNo;

    private void Start()
    {
        
        if (gameObject.name[0] == 'R')
        {
            wayPointsParent = GameObject.FindWithTag("RoadWayPoints2");
        }
        if (gameObject.name[0] == 'L')
        {
            wayPointsParent = GameObject.FindWithTag("RoadWayPoints1");
        }
        
        wayPoints = wayPointsParent.GetComponentsInChildren<Transform>();
        wayPointNo = firstWayPoint;
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate the vehicle/wheels in the direct of next waypoint
        Quaternion lookRotation = Quaternion.LookRotation(wayPoints[wayPointNo].position - transform.position);
        
        rightWheel.transform.rotation = Quaternion.RotateTowards(rightWheel.transform.rotation, lookRotation,rotationSpeed *Time.deltaTime);
        leftWheel.transform.rotation = Quaternion.RotateTowards(leftWheel.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        
        //Move vehicle forward
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[wayPointNo].position, movementSpeed * Time.deltaTime);
        
        //set the next waypoint
        if (Mathf.Abs(wayPoints[wayPointNo].position.z - transform.position.z) <= 1f && Mathf.Abs(wayPoints[wayPointNo].position.x - transform.position.x) <= 1f)
        {
            wayPointNo++;
        }
        
        //when the car reach the last wayPoint it should go to the first one and start movement from the first wayPoint
        if (wayPointNo == wayPoints.Length)
        {
            wayPointNo = 1;
            Debug.Log("Move to first position");
        }

    }
}
