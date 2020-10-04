using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Weapon
{
    public Light beeper;
    public float beeperSpeed;
    public float armTime;
    public ParticleSystem explosionSystem;
    SphereCollider explosionRadiusTrigger;

    // Start is called before the first frame update
    void Start()
    {
        explosionRadiusTrigger = GetComponent<SphereCollider>();
        explosionRadiusTrigger.enabled = false;
    }

    override public void fire(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        StartCoroutine(armMine());
    }

    IEnumerator armMine()
    {
        yield return new WaitForSeconds(armTime);
        explosionRadiusTrigger.enabled = true;
        StartCoroutine(armed());
    }

    IEnumerator armed()
    {
        while (true)
        {
            beeper.intensity = Mathf.PingPong(Time.time * beeperSpeed, 1);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            Debug.Log(other);
            CarData carDataScript = other.gameObject.transform.parent.GetComponent<CarData>();
            carDataScript.takeDamage(damage);
            explosionSystem.Play();
            foreach (Transform t in transform)
            {
                if(t.gameObject != explosionSystem.gameObject)
                    t.gameObject.SetActive(false);
            }
            Destroy(this.gameObject, explosionSystem.main.duration);

        }
    }
}
