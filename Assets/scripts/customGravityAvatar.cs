using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customGravityAvatar : MonoBehaviour
{
    Rigidbody rb;
    public float gravity = 2;
    public int walkSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
            processPlayerInput();
    }

    private void processPlayerInput()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementForce = transform.forward * verticalInput + transform.right * horizontalInput;
        rb.AddForce(movementForce * walkSpeed, ForceMode.Acceleration);

        if (horizontalInput != 0 || verticalInput != 0) {
            transform.RotateAround(transform.position, transform.up, horizontalInput*90);
        }

        //jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * gravity * 120, ForceMode.Force);
        }
    }
}
