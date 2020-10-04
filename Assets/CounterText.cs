using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class CounterText : MonoBehaviour
{
    public string labelText;
    public int initialValue = 0;

    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
        Set(initialValue);
    }

    public void Set(int value)
    {
        _text.text = labelText + ": " + value;
    }
}
