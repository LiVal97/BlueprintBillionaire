using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorMovement : MonoBehaviour
{
    public GameObject tractor;
    public GameObject rightWheel;
    public GameObject leftWheel;
    public GameObject rightRearWheel;
    public GameObject leftRearWheel;
    public GameObject wayPointsParent;

    public float rotationSpeed;
    private float wheelRotationSpeed;
    public float movementSpeed;
    
    public Transform[] wayPoints;
    
    public int firstWayPoint;
    public int wayPointNo;
    
    // Start is called before the first frame update
    void Start()
    {
        wayPoints = wayPointsParent.GetComponentsInChildren<Transform>();
        wayPointNo = firstWayPoint;
        wheelRotationSpeed = rotationSpeed * 2;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion lookRotation = Quaternion.LookRotation(wayPoints[wayPointNo].position - tractor.transform.position);

        rightWheel.transform.rotation = Quaternion.RotateTowards(rightWheel.transform.rotation, 
            lookRotation,wheelRotationSpeed *Time.deltaTime);
        
        leftWheel.transform.rotation = Quaternion.RotateTowards(leftWheel.transform.rotation, 
            lookRotation, wheelRotationSpeed * Time.deltaTime);
        
        tractor.transform.rotation = Quaternion.RotateTowards(tractor.transform.rotation, lookRotation,
            rotationSpeed * Time.deltaTime);

        tractor.transform.position = Vector3.MoveTowards(tractor.transform.position,
            wayPoints[wayPointNo].position, movementSpeed * Time.deltaTime);

        rightRearWheel.transform.Rotate(1,0,0);
        leftRearWheel.transform.Rotate(1,0,0);

        if (Mathf.Abs(wayPoints[wayPointNo].position.z - tractor.transform.position.z) <= 1f && Mathf.Abs(wayPoints[wayPointNo].position.x - tractor.transform.position.x) <= 1f)
        {
            wayPointNo++;
        }
        
        if (wayPointNo == wayPoints.Length)
        {
            wayPointNo = 0;
            Debug.Log("Tractor move to first position");
        }
    }
    
    
    
}
