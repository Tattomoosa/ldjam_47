using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float acceleration = 1000.0f;
    public float turnAcceleration = 100.0f;

    private bool isGrounded = true;
    private Rigidbody _rb;
    // TODO base class CarInput
    private CarInputPlayer _input;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _input = GetComponent<CarInputPlayer>();
        _rb.maxAngularVelocity = 10.0f;
    }

    private void FixedUpdate()
    {
        Vector2 steeringInput = _input.GetInput();
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        
        Debug.Log(steeringInput);

        if (isGrounded)
        {
            _rb.AddForce(forward * (steeringInput.y * acceleration));
            // _rb.AddTorque(Vector3.up * (steeringInput.x * turnAcceleration), ForceMode.Force);
            // _rb.AddTorque(right * (steeringInput.x * turnAcceleration));
        }
    }
}
