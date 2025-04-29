using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_to_school : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 20f;
    private Rigidbody rb;
    [SerializeField] private bool isRide =false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (isRide) {
            float moveInput = Input.GetAxis("Vertical"); // W/S Å° ÀÔ·Â
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(transform.forward * moveInput * acceleration, ForceMode.Acceleration);
            }
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
