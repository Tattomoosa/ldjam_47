using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BombTrajectory : MonoBehaviour
{
    public Vector3 initalVelocity;
    Vector3 carVelocity;
    public Rigidbody rbCar;
    LineRenderer lineRenderer;
    public GameObject ammo;
    public float force;
    public Transform spawnPoint;
    public GameObject gun;

    void Start()
    {
        initalVelocity = spawnPoint.up;
        initalVelocity.Normalize();
        initalVelocity *= force;
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            GameObject g = Instantiate(ammo, spawnPoint.position, spawnPoint.rotation);
            Rigidbody rb = g.GetComponent<Rigidbody>();
            rb.velocity = initalVelocity + carVelocity;
        }
    }
    private void FixedUpdate()
    {
        carVelocity = rbCar.velocity;
        initalVelocity = spawnPoint.up;
        initalVelocity.Normalize();
        initalVelocity *= force;
        //Time of flight calculation

        float t;
        t = (-1f * initalVelocity.y) / Physics.gravity.y;
        t = 2f * t;

        //Trajectory calculation

        lineRenderer.positionCount = 100;
        Vector3 trajectoryPoint;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            //
            float time = t * i / (float)(lineRenderer.positionCount);
            trajectoryPoint = spawnPoint.position + initalVelocity * time + 0.5f * Physics.gravity * time * time;
            lineRenderer.SetPosition(i, trajectoryPoint);
        }
/*        if (Input.GetMouseButtonUp(0))
        {
            GameObject g = Instantiate(ammo, spawnPoint.position, spawnPoint.rotation);
            Rigidbody rb = g.GetComponent<Rigidbody>();
            rb.velocity = initalVelocity + carVelocity;
        }*/
    }
}