using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> weapons;
    public List<Transform> spawnLocations;
    int numOfWeapons;
    GameObject curWeapon;

    // Start is called before the first frame update
    void Start()
    {
        numOfWeapons = weapons.Count;
        if(numOfWeapons > 0)
        {
            curWeapon = weapons[0];
        }
        else
        {
            curWeapon = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkInput();
    }

    void checkInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(curWeapon != null)
            {
                GameObject g = Instantiate<GameObject>(curWeapon);
                Weapon weaponScript = g.GetComponent<Weapon>();
                weaponScript.fire(spawnLocations[weapons.IndexOf(curWeapon)].position);
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if(curWeapon != null)
            {
                curWeapon = weapons[(weapons.IndexOf(curWeapon) + 1) % numOfWeapons];
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (curWeapon != null)
            {
                curWeapon = weapons[(weapons.IndexOf(curWeapon) - 1) % numOfWeapons];
            }
        }
    }
}
