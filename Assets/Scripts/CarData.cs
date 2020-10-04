using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarData : MonoBehaviour
{
    ArcadeCarController arcadeCarController;
    public int health = 10;
    int curHealth;
    public int respawnTime;

    public List<GameObject> carObjects;

    // Start is called before the first frame update
    void Start()
    {
        curHealth = health;
        arcadeCarController = GetComponent<ArcadeCarController>();
    }

    public void takeDamage(int incomingDamage)
    {
        curHealth -= incomingDamage;
        if(curHealth <= 0)
        {
            die();
        }
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
    }
}
