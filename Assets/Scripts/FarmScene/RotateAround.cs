using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
public float rotationSpeed;
public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target.transform.RotateAround(target.transform.position, Vector3.back, rotationSpeed *Time.deltaTime);
    }
}
