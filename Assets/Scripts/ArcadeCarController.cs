using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore;

public class ArcadeCarController : MonoBehaviour
{
    public Rigidbody motionTargetRigidbody;
    public Transform modelTransform;
    
    [Header("Vehicle Stats")]
    
    public float forwardAccel = 8.0f;
    public float reverseAccel = 4.0f;
    public float turnStrength = 180.0f;
    public float dragOnGround = 2.0f;
    public float extraFallingGravity = 10.0f;
    public float maxWheelTurn = 20.0f;
    public float brakeStrength = 80.0f;

    [Header("Wheels")]
    
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform rearLeftWheel;
    public Transform rearRightWheel;

    [Header("Ground Check")]
    
    public LayerMask whatIsGround;
    public Transform groundCheckOrigin;
    public float groundRayLength = 1.0f;

    [Header("Sound")]
    public float maxDriveSpeed = 50.0f;

    public float maxPitch = 1.0f;
    // TODO below this idle, set a bit higher
    public float minDriveSpeed = 0.0f;
    public float minPitch = -2.0f;
    public AudioSource engineSound;

    private CarInput _input;
    private bool _grounded;
    private float _speedInput;
    private float _turnInput;
    private float _speedFrame;
    private bool _brakeInput;
    private Vector3 _motionTargetOffset;

    

    void Start()
    {
        _input = GetComponent<CarInput>();
        motionTargetRigidbody.transform.parent = null;
        motionTargetRigidbody.drag = dragOnGround;
        _motionTargetOffset = transform.position - motionTargetRigidbody.transform.position ;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(groundCheckOrigin.position, -transform.up * groundRayLength);
    }

    void Update()
    {
        var input = _input.GetInput();
        _speedFrame = 0.0f;
        // TODO just use input fields instead of new variables...
        _speedInput = input.VerticalSteeringAxis;
        _turnInput = input.HorizontalSteeringAxis;
        _brakeInput = input.IsBraking;
        
            
        // update car rotation
        // TODO THIS IS KINDA JANK cuz it depends on if player is pressing forward/back
        // should instead depend on actual speed
        if (_grounded)
        {
            transform.rotation = Quaternion.Euler(
                transform.rotation.eulerAngles +
                new Vector3(0f, _turnInput * _speedInput * turnStrength * Time.deltaTime, 0f)
            );
        }

        // rotate wheels
        Vector3 frontLeftWheelEuler = frontLeftWheel.localRotation.eulerAngles;
        Vector3 frontRightWheelEuler = frontRightWheel.localRotation.eulerAngles;
        frontLeftWheelEuler.y = (_turnInput * maxWheelTurn) - 180;
        frontRightWheelEuler.y = (_turnInput * maxWheelTurn) - 180;
        frontLeftWheel.localRotation = Quaternion.Euler(frontLeftWheelEuler);
        frontRightWheel.localRotation = Quaternion.Euler(frontRightWheelEuler);


        // Apply forces to speedInput for rigidbody
        if (_speedInput > 0)
        {
            _speedFrame = _speedInput * forwardAccel * 1000f;
        }
        else if (_speedInput < 0)
            _speedFrame = _speedInput * reverseAccel * 1000f;
        
        // sound
        var soundVelocity = motionTargetRigidbody.velocity;
        soundVelocity.y = 0;
        var soundRatio = Mathf.InverseLerp(minDriveSpeed, maxDriveSpeed, soundVelocity.magnitude);
        engineSound.pitch = Mathf.Lerp(minPitch, maxPitch, soundRatio);
        
        
        // set location to rigidbody
        transform.position = motionTargetRigidbody.transform.position + _motionTargetOffset;
    }

    private void FixedUpdate()
    {
        _grounded = false;
        RaycastHit hit;
        Debug.DrawRay(groundCheckOrigin.position, -transform.up * groundRayLength);
        if (Physics.Raycast(groundCheckOrigin.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            _grounded = true;
            // rotate parallel to ground
            modelTransform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if (_grounded)
        {
            motionTargetRigidbody.drag = dragOnGround;
            if (_brakeInput)
                motionTargetRigidbody.AddForce(brakeStrength * -motionTargetRigidbody.velocity);
            if (Mathf.Abs(_speedInput) > 0)
                motionTargetRigidbody.AddForce(transform.forward * _speedFrame);
        }
        else
        {
            motionTargetRigidbody.drag = 0.1f;
            motionTargetRigidbody.AddForce(Vector3.down * (extraFallingGravity * 1000f));
        }

    }
}
