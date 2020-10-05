using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarData : MonoBehaviour
{
    ArcadeCarController arcadeCarController;
    public int health = 10;
    int curHealth;
    public int respawnTime;

    public List<GameObject> carObjects;
    public GameObject cube;
    public GameObject frontLeftWheel;
    public GameObject RearLeftWheel;
    public GameObject FrontRightWheel;
    public GameObject RearRightWheel;
    public GameObject destroyedCube;
    public GameObject destroyedFrontLeftWheel;
    public GameObject destroyedRearLeftWheel;
    public GameObject destroyedFrontRightWheel;
    public GameObject destroyedRearRightWheel;
    public GameObject destroyedCar;
    Vector3[] velhiclePositions;
    public float explosionForce;

    // ok this is kinda dumb but otherwise we need ui reference
    public UnityEvent<float, float, float> setDamageUI;

    // Start is called before the first frame update
    void Start()
    {
        velhiclePositions = new Vector3[5];
        curHealth = health;
        arcadeCarController = GetComponent<ArcadeCarController>();
        UpdateUI();
    }

    public void updateVelhiclePositions()
    {
        velhiclePositions[0] = cube.transform.position;
        velhiclePositions[1] = frontLeftWheel.transform.position;
        velhiclePositions[2] = RearLeftWheel.transform.position;
        velhiclePositions[3] = FrontRightWheel.transform.position;
        velhiclePositions[4] = RearRightWheel.transform.position;
    }

    public void takeDamage(int incomingDamage)
    {
        curHealth -= incomingDamage;
        if(curHealth <= 0)
        {
            die();
        }
        UpdateUI();
    }

    public void die()
    {
        arcadeCarController.enabled = false;
        foreach(GameObject g in carObjects)
        {
            g.SetActive(false);
        }
        destroyedCar.SetActive(true);
        Rigidbody[] r = destroyedCar.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody a in r)
        {
            a.useGravity = true;
            a.AddExplosionForce(explosionForce, a.transform.parent.position, explosionForce);
        }
        updateVelhiclePositions();
        StartCoroutine(respawn());
    }

    IEnumerator respawn()
    {
        foreach(GameObject g in carObjects)
        {
            g.SetActive(false);
        }
        yield return new WaitForSeconds(respawnTime);
        foreach (GameObject g in carObjects)
        {
            g.SetActive(true);
        }
        arcadeCarController.enabled = true;
        curHealth = health;
        foreach(GameObject g in carObjects)
        {
            g.SetActive(true);
        }
        destroyedCar.SetActive(false);
        destroyedCube.transform.position = velhiclePositions[0];
        destroyedFrontLeftWheel.transform.position = velhiclePositions[1];
        destroyedRearLeftWheel.transform.position = velhiclePositions[2];
        destroyedFrontRightWheel.transform.position = velhiclePositions[3];
        destroyedRearRightWheel.transform.position = velhiclePositions[4];
        UpdateUI();
    }

    public void UpdateUI()
    {
        setDamageUI.Invoke(0, health, curHealth);
    }
}
