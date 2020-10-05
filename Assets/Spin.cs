using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public Vector3 around = Vector3.up;
    public float degreesPerSecond = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(around, degreesPerSecond * Time.deltaTime);
    }
}
