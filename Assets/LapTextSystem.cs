using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTextSystem : MonoBehaviour
{
    public GameObject textBox;
    public Text text;
    
    public List<LapText> lapText = new List<LapText>();

    private Dictionary<int, string> _lapTextDict = new Dictionary<int, string>();

    void Awake()
    {
        textBox.SetActive(false);
        foreach (LapText lt in lapText)
            _lapTextDict[lt.lap] = lt.text;
    }

    public void OnLap(int lap)
    {
        if (!_lapTextDict.ContainsKey(lap))
            return;
        if (!textBox.activeSelf)
            textBox.SetActive(true);
        text.text = _lapTextDict[lap];
    }

    [System.Serializable]
    public struct LapText
    {
        public int lap;
        [TextArea]
        public string text;
    }
}
