using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySource : MonoBehaviour
{
    public int priority = 1;
    public Vector3 position;

    public bool gravityPullsDown = true;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    public Vector3 computeGravity(Vector3 otherPos) {
        Vector3 originOfGravity = otherPos;

        RaycastHit hit;

        Physics.Raycast(origin: otherPos, direction: position - otherPos, maxDistance: 100f, hitInfo: out hit, layerMask: LayerMask.GetMask("GravitySource"));
        Debug.DrawLine(originOfGravity, hit.point, Color.red);
        Debug.DrawLine(hit.point, hit.point + hit.normal, Color.green);
        Debug.Log(hit.normal);
        return gravityPullsDown ? -hit.normal : hit.normal;

    }
}
