using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> weapons;
    public List<Transform> spawnLocations;
    int numOfWeapons;
    GameObject curWeapon;
    private CarInput _input;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<CarInput>();
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
        if(Time.timeScale != 0)
            checkInput();
    }

    void checkInput()
    {
        InputState inputState = _input.GetInput();
        if (inputState.Fire)
        {
            if(curWeapon != null)
            {
                GameObject g = Instantiate<GameObject>(curWeapon);
                Weapon weaponScript = g.GetComponent<Weapon>();
                weaponScript.fire(spawnLocations[weapons.IndexOf(curWeapon)].position);
            }
        }
        else if (inputState.SelectNextWeapon)
        {
            if(curWeapon != null)
            {
                curWeapon = weapons[(weapons.IndexOf(curWeapon) + 1) % numOfWeapons];
            }
        }
        else if (inputState.SelectPrevWeapon)
        {
            if (curWeapon != null)
            {
                curWeapon = weapons[(weapons.IndexOf(curWeapon) - 1) % numOfWeapons];
            }
        }
    }
}
