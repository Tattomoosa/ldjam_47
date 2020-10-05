using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapHighScoreText : MonoBehaviour
{
    private Text _text;
    void Start()
    {
        _text = GetComponent<Text>();
        if (PlayerPrefs.HasKey("hiScore"))
        {
            int hiScore = PlayerPrefs.GetInt("hiScore");
            int randomHigher = Random.Range(2, 5);
            if (hiScore > 0)
            {
                _text.text = "You made it to lap " + hiScore + "." +
                             " I bet something good happens at lap " +
                             (hiScore + randomHigher) + "!";
            }
        }
        else
            _text.text = "";
    }
}
