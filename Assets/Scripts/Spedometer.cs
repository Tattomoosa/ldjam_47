using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spedometer : MonoBehaviour
{
    // public float currentValue = 30.0f;
    public float minValue = 0.0f;
    public float maxValue = 100.0f;
    public float minRotation = -145.0f;
    public float maxRotation = 145.0f;
    public int textPoints = 12;
    public float innerTextRadius = 100.0f;

    public Rigidbody target;
    public RectTransform handleRootTransform;
    public Text textBasis;

    void Awake()
    {
        textBasis = GetComponentInChildren<Text>();
        
        // this isn't really right but it'll wrok
        float degreesTotal = minRotation - maxRotation;
        float degreesBetween = degreesTotal / textPoints;
        for (int i = 0; i < textPoints; ++i)
            CreateTextAtDegrees(minRotation - (degreesBetween * i));
        Destroy(textBasis);
    }

    void CreateTextAtDegrees(float degrees)
    {
        var rotationLerp = Mathf.InverseLerp(minRotation, maxRotation, degrees);
        var value = Mathf.Lerp(minValue, maxValue, rotationLerp);
        Vector3 noRotation = Vector3.up * innerTextRadius;
        Vector3 rotation = Quaternion.AngleAxis(degrees, Vector3.forward) * noRotation;
        var text = Object.Instantiate(textBasis);
        text.transform.SetParent(this.transform);
        text.transform.localPosition = rotation;
        // text.text = Mathf.FloorToInt(value).ToString();
        var mphValue = value * 0.447f;
        text.text = (Mathf.Round(value / 5) * 5).ToString();
    }

    void Update()
    {
        Vector3 v = target.velocity;
        v.y = 0;
        float currentValue = v.magnitude;
        float currentRotationLerp = Mathf.InverseLerp(minValue, maxValue, currentValue);
        float rotationValue = Mathf.Lerp(minRotation, maxRotation, currentRotationLerp);
        handleRootTransform.rotation = Quaternion.Euler(0, 0, rotationValue);
    }
}
