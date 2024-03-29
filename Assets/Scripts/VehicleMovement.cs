using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public GameObject[] wayPoints;
    public GameObject rightWheel;
    public GameObject leftWheel;
    public float rotationSpeed;
    
    public int wayPointNo = 0;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(wayPointNo);
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[wayPointNo].transform.position, 5 * Time.deltaTime);
        Quaternion lookRotation = Quaternion.LookRotation(wayPoints[wayPointNo].transform.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        
        if (Mathf.Abs(wayPoints[wayPointNo].transform.position.z - transform.position.z) < 2f && Mathf.Abs(wayPoints[wayPointNo].transform.position.x - transform.position.x) < 2f)
        {
            if (wayPointNo == wayPoints.Length-1)
            {
                wayPointNo = 0;
                Debug.Log("Move to first position");
            }
            wayPointNo++;
        }

    }
}
