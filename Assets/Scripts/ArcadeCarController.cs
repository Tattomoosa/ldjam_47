using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore;

public class ArcadeCarController : MonoBehaviour
{
    [FormerlySerializedAs("rigidbody")] public Rigidbody motionTargetRigidbody;

    public float forwardAccel = 8.0f;
    public float reverseAccel = 4.0f;
    public float maxSpeed = 50.0f;
    public float turnStrength = 180.0f;
    public float extraFallingGravity = 10.0f;
    public float groundRayLength = 1.0f;
    public float dragOnGround = 3.0f;
    public float maxWheelTurn = 20.0f;

    public Transform frontLeftWheel, frontRightWheel, rearLeftWheel, rearRightWheel;

    public LayerMask whatIsGround;
    public Transform groundCheckOrigin;

    [SerializeField]
    private bool _grounded;
    private float _speedInput;
    private float _turnInput;
    private float _speedFrame;

    private Vector3 _motionTargetOffset;
    

    void Start()
    {
        motionTargetRigidbody.transform.parent = null;
        _motionTargetOffset = transform.position - motionTargetRigidbody.transform.position ;
    }

    void Update()
    {
        _speedFrame = 0.0f;
        _speedInput = Input.GetAxis("Vertical");
        _turnInput = Input.GetAxis("Horizontal");
        
            
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
            _speedFrame = _speedInput * forwardAccel * 1000f;
        else if (_speedInput < 0)
            _speedFrame = _speedInput * reverseAccel * 1000f;
        
        
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
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        if (_grounded)
        {
            motionTargetRigidbody.drag = dragOnGround;
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
