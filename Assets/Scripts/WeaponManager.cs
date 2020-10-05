using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> weapons;
    public List<Transform> spawnLocations;
    int numOfWeapons;
    GameObject curWeapon;
    Weapon curWeaponScript;
    private CarInput _input;
    float timeToFire;
    public Text countDownTimer;
    public UnityEvent<float,float,float> onCountdown;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<CarInput>();
        numOfWeapons = weapons.Count;
        if(numOfWeapons > 0)
        {
            curWeapon = weapons[0];
            curWeaponScript = curWeapon.GetComponentInChildren<Weapon>();
        }
        else
        {
            curWeapon = null;
            curWeaponScript = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale != 0)
        {
            timeToFire += Time.deltaTime;
            timeToFire = Mathf.Clamp(timeToFire, 0f, curWeaponScript.delay);
            if(gameObject.GetComponent<CarInputPlayer>() != null)
            {
                // countDownTimer.text = timeToFire == curWeaponScript.delay ? "Fire!" : (curWeaponScript.delay - timeToFire).ToString();
                onCountdown.Invoke(0, curWeaponScript.delay, timeToFire);
            }
            checkInput();
        }
    }

    void checkInput()
    {
        InputState inputState = _input.GetInput();
        if (inputState.Fire)
        {
            if(curWeapon != null && timeToFire >= curWeaponScript.delay)
            {
                GameObject g = Instantiate<GameObject>(curWeapon);
                Weapon weaponScript = g.GetComponentInChildren<Weapon>();
                weaponScript.fire(spawnLocations[weapons.IndexOf(curWeapon)].position);
                timeToFire = 0f;
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
