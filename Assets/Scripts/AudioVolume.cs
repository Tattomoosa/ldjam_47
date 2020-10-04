using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioVolume
{
    private static float _globalVolume = 0.5f;
    public static float GlobalVolume
    {
        get => _globalVolume;
        set
        {
            float volume = Mathf.Clamp(value, 0.0f, 1.0f);
            AudioListener.volume = volume;
            PlayerPrefs.SetFloat("globalVolume", volume);
            _globalVolume = volume;
            
        }
    }

    public static void Init()
    {
        if (PlayerPrefs.HasKey("globalVolume"))
            GlobalVolume = PlayerPrefs.GetFloat("globalVolume");
        else
            GlobalVolume = 0.5f;
    }
}
