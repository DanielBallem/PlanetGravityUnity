using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GravityType { 
    Sphere,
    Capsule,
    Cube
}
public class GravitySource : MonoBehaviour
{
    public GravityType gravity;
    public int priority = 1;
    public Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    Vector3[] possibleDirectionsForCube = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

    public Vector3 GravityFromPosition(Vector3 position) {
        switch (gravity) { 
            case GravityType.Sphere:
                return (this.transform.position - position).normalized;
                break;
            case GravityType.Capsule:
                return Vector3.up;
                break;
            case GravityType.Cube:
                return CalculateCubeGravity(position);
                break;
            default:
                return Vector3.down;
        }
    }

    private Vector3 CalculateCubeGravity(Vector3 position)
    {
        Vector3 difference = this.transform.position - position;
        Vector3 desiredDirection = Vector3.zero;
        float largestDot = float.MinValue;
        foreach (Vector3 direction in possibleDirectionsForCube) { 
            float curr = Vector3.Dot(direction, difference);
            if (curr > largestDot) {
                desiredDirection = direction;
                largestDot = curr;
            }
        }

        return desiredDirection;
    }
}
