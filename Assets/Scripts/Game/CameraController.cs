using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private float moveSpeed = 30f;
    public float topBound;
    public float bottomBound;
    public float leftBound;
    public float rightBound;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - 20f)
        {
            position.z += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= 20f)
        {
            position.z -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - 20f)
        {
            position.x += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= 20f)
        {
            position.x -= moveSpeed * Time.deltaTime;
        }

        position.x = Mathf.Clamp(position.x, leftBound, rightBound);
        position.z = Mathf.Clamp(position.z, bottomBound, topBound);
        
        transform.position = position;
    }
}
