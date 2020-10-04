using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStartSystem : MonoBehaviour
{
    public bool skipStartupSequence = false;
    public float waitBefore = 3.0f;
    public float waitBetween = 1.0f;
    public GameObject carParent;
    public List<GameObject> lights;
    private ArcadeCarController[] _cars;
    WeaponManager[] weaponManagers;
    
    void Start()
    {
        _cars = carParent.GetComponentsInChildren<ArcadeCarController>();
        weaponManagers = carParent.GetComponentsInChildren<WeaponManager>();
        if (!skipStartupSequence)
            StartCoroutine(StartupSequence());
    }

    IEnumerator StartupSequence()
    {
        foreach (GameObject light in lights)
            light.SetActive(false);
        foreach (var car in _cars)
            car.enabled = false;
        foreach (var weaponManager in weaponManagers)
            weaponManager.enabled = false;                
        yield return new WaitForSeconds(waitBefore);
        // foreach (GameObject light in lights)
        for (int i = 0; i < lights.Count; ++i)
        {
            lights[i].SetActive(true);
            // TODO play sound
            if (i < lights.Count - 1)
                yield return new WaitForSeconds(waitBetween);
        }
        foreach (var car in _cars)
            car.enabled = true;
        foreach (var weaponManager in weaponManagers)
            weaponManager.enabled = true;
    }
}
