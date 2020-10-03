using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpedometerFromRigidbody : MonoBehaviour
{
    public Spedometer spedometer;
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO this should do something smarter
        // Debug.Log(_rb.velocity.magnitude);
        Vector3 velocity = _rb.velocity;
        velocity.y = 0;
        spedometer.currentValue = velocity.magnitude;
    }
}
