using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class OrchardCharMovement : MonoBehaviour
{
    [SerializeField] private GameObject emptyBox;
    [SerializeField] private GameObject fullBox;

    public float movementSpeed;
    public float rotationSpeed;

    public Transform[] waypoint;
    private int wayPointNo;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        wayPointNo = 0;
        anim = GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion lookRotation = Quaternion.LookRotation(waypoint[wayPointNo].position - transform.position);
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, waypoint[wayPointNo].position,
            movementSpeed * Time.deltaTime);

        if (Mathf.Abs(waypoint[wayPointNo].position.z - transform.position.z) <= 0.5f && Mathf.Abs(waypoint[wayPointNo].position.x - transform.position.x) <= 0.5f)
        {
            wayPointNo++;
        }

        if (wayPointNo > 0)
        {
            fullBox.SetActive(true);
            emptyBox.SetActive(false);
        }

        if (wayPointNo == 0)
        {
            fullBox.SetActive(false);
            emptyBox.SetActive(true);
        }

        if (wayPointNo == waypoint.Length)
        {
            wayPointNo = 0;
        }
    }
}
