using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spedometer : MonoBehaviour
{
    public float currentValue = 30.0f;
    public float minValue = 0.0f;
    public float maxValue = 100.0f;
    public float minRotation = 145.0f;
    public float maxRotation = -145.0f;
    
    public RectTransform handleRootTransform;

    void Start()
    {
        // _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        float currentRotationLerp = Mathf.InverseLerp(minValue, maxValue, currentValue);
        float rotationValue = Mathf.Lerp(minRotation, maxRotation, currentRotationLerp);
        handleRootTransform.rotation = Quaternion.Euler(0, 0, rotationValue);
    }
}
