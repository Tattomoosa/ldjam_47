using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public Light beeper;
    public float speed;
    ParticleSystem explosionSystem;

    // Start is called before the first frame update
    void Start()
    {
        explosionSystem = GetComponent<ParticleSystem>();    
    }

    // Update is called once per frame
    void Update()
    {
        beeper.intensity = Mathf.PingPong(Time.time * speed, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Car")
        {
            foreach(Transform t in transform)
            {
                t.gameObject.SetActive(false);
            }    
        }
        explosionSystem.Play();
        Destroy(this, explosionSystem.main.duration);
    }
}
