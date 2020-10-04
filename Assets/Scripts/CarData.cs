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
    public GameObject destroyedCar;

    // ok this is kinda dumb but otherwise we need ui reference
    public UnityEvent<float, float, float> setDamageUI;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = health;
        arcadeCarController = GetComponent<ArcadeCarController>();
        UpdateUI();
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
        UpdateUI();
    }

    public void UpdateUI()
    {
        setDamageUI.Invoke(0, health, curHealth);
    }
}
