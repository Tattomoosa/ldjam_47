using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Slider>().value = AudioVolume.GlobalVolume;
    }
    public void SetGlobalVolume(float volume)
    {
        AudioVolume.GlobalVolume = volume;
    }
}
