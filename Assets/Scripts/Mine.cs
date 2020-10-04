using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Weapon
{
    public Light beeper;
    public float beeperSpeed;
    public float armTime;
    public ParticleSystem explosionSystem;
    public SphereCollider explosionRadiusTrigger;

    // Start is called before the first frame update
    void Start()
    {
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
        CarData carDataScript = other.gameObject.transform.GetComponent<CarData>();
        explosionRadiusTrigger.enabled = false;
        carDataScript.takeDamage(damage);
        // explosionSystem.Play();
        // now sound is on object too so i just set playOnAwake and disabled it and changed this - matt
        explosionSystem.gameObject.SetActive(true);
        foreach (Transform t in transform.parent)
        {
            if(t.gameObject != explosionSystem.gameObject && t.gameObject != gameObject)
                t.gameObject.SetActive(false);
        }


        Destroy(this.gameObject, explosionSystem.main.duration);
    }
}
