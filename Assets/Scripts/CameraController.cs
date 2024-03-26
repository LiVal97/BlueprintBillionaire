using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private float moveSpeed = 20f;
    public Vector2 bounds;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - 10f)
        {
            position.z += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= 10f)
        {
            position.z -= moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - 10f)
        {
            position.x += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= 10f)
        {
            position.x -= moveSpeed * Time.deltaTime;
        }

        position.x = Mathf.Clamp(position.x, -bounds.x, bounds.x);
        position.z = Mathf.Clamp(position.z, -bounds.y, bounds.y);
        
        transform.position = position;
    }
}
