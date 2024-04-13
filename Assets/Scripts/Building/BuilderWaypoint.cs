using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderWaypoint : MonoBehaviour
{
    public bool isOcupied;

    public GameObject carryObject;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}
