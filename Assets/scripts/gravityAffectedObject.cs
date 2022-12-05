using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityAffectedObject : MonoBehaviour
{

    Rigidbody rb;
    public float gravity = 2;
    public Vector3 gravityDir = Vector3.zero;
    List<GravitySource> gravitySources = new List<GravitySource>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gravitySources.Count > 0)
        {
            gravityDir = gravityDirection();
            rb.AddForce(gravityDir * gravity, ForceMode.Acceleration);
            transform.up = -gravityDir;
        }
    }

    private Vector3 gravityDirection()
    {
        GravitySource[] sources = gravitySources.ToArray();
        GravitySource source = sources[0];
        if (source != null)
        {
            return source.GravityFromPosition(transform.position);
        }
        return Vector3.zero;
    }

    private void SortGravitySources()
    {
        gravitySources.Sort(delegate (GravitySource x, GravitySource y)
        {
            if (x.priority < y.priority)
            {
                return 1;
            }
            else if (x.priority > y.priority)
            {
                return -1;
            }
            //equal priority. sort by closest one.
            else
            {
                if (Vector3.Distance(x.position, this.transform.position) <= Vector3.Distance(y.position, this.transform.position))
                {
                    return -1;
                }
                return 1;
            }
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GravitySource")
        {
            gravitySources.Add(other.gameObject.GetComponent<GravitySource>());
            SortGravitySources();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "GravitySource")
            gravitySources.Remove(other.gameObject.GetComponent<GravitySource>());
    }
}
