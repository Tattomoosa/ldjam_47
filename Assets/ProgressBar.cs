using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public float min = 0;
    public float max = 10;
    public float initial = 5;
    public RectTransform barFill;

    public bool shrinkLeft = false;

    private RectTransform _rect;
    
    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        // Debug.Log("BAR: " + barFill.name);
        // Set(initial);
    }

    public void Set(float value)
    {
        float percentFilled = 1.0f - Mathf.InverseLerp(min, max, value);
        // float pixelsFilled = Mathf.Lerp(0.0f, rect.width, percentFilled);

        if (shrinkLeft)
        {
            barFill.anchorMin = Vector2.zero;
            barFill.anchorMax = new Vector2(percentFilled, 1.0f);
        }
        else
        {
            barFill.anchorMax = Vector2.one;
            barFill.anchorMin = new Vector2(percentFilled, 0.0f);
        }
    }

    public void SetWithMinMax(float newMin, float newMax, float value)
    {
        min = newMin;
        max = newMax;
        Set(value);
    }
}
