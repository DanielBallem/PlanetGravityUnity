using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityAffectedObject : MonoBehaviour
{

    Rigidbody rb;
    public float gravity = 2;
    public Vector3 gravityDir = Vector3.zero;
    GravitySource activeGravitySource;
    public float rotationSpeed = 1;
    
    int highestCurrPriority = int.MinValue;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeGravitySource)
        {
            gravityDir = activeGravitySource.computeGravity(transform.position);
            rb.AddForce(gravityDir * gravity, ForceMode.Acceleration);
            //transform.up = -gravityDir;
            transform.up = Vector3.Lerp(transform.up, -gravityDir, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GravitySource")
        {
            GravitySource gs = other.gameObject.GetComponent<GravitySource>();

            if (gs == null) return;
            SetHighestPriorityGravitySource(gs);
        }
    }

    private void SetHighestPriorityGravitySource(GravitySource gs)
    {
        if (gs.priority > highestCurrPriority)
        {
            SetGravitySource(gs);
        }
        else if (gs.priority == highestCurrPriority)
        {
            if (Vector3.Distance(activeGravitySource.position, transform.position) > Vector3.Distance(gs.position, transform.position))
            {
                SetGravitySource(gs);
            }
        }
    }

    private void SetGravitySource(GravitySource gs)
    {
        activeGravitySource = gs;
        highestCurrPriority = gs.priority;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("GravitySource")) {
            GravitySource gs = other.gameObject.GetComponent<GravitySource>();
            SetHighestPriorityGravitySource(gs);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("GravitySource")) {
            GravitySource gs = other.gameObject.GetComponent<GravitySource>();

            if (gs == activeGravitySource) { 
                activeGravitySource = null;
                highestCurrPriority = int.MinValue;
            }
        }
    }
}
