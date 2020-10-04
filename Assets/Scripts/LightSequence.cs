using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSequence : MonoBehaviour
{
    public List<Material> lightMats;
    public int maxIntensity;
    public float speedLightUp;

    private void Start()
    {
        StartCoroutine(glowUp());
    }

    IEnumerator glowUp()
    {
        float t = 0f;
        foreach(Material curLightMat in lightMats)
        {
            while(curLightMat.GetColor("_EmissionColor") != new Color(255,255,255))
            {
                curLightMat.SetColor("_EmissionColor", curLightMat.GetColor("EmissionColor") * speedLightUp * Time.deltaTime);
                yield return null;
            }
        }
    }
}
