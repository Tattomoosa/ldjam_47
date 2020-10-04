using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWithKey : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.None;
    public GameObject toggleObject;

    public bool doToggleTime = false;

    private float previousTimeScale;
    // Start is called before the first frame update

    void Awake()
    {
        previousTimeScale = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyCode == KeyCode.None)
            return;
        if (!Input.GetKeyDown(keyCode))
            return;
        toggleObject.SetActive(!toggleObject.activeSelf);
        if (doToggleTime)
        {
            if (Time.timeScale == 0.0f)
                Time.timeScale = previousTimeScale;
            else
            {
                previousTimeScale = Time.timeScale;
                Time.timeScale = 0.0f;
            }
        }
    }
}
