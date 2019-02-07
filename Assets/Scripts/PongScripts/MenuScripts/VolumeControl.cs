using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public UnityEngine.Audio.AudioMixer mixer;
    public Slider slider;

    private float newVol;

    // Start is called before the first frame update
    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = 100;
        slider.value = GlobalVariables.MasterVolume + 80;
    }

    // Update is called once per frame
    void Update()
    {
        newVol = slider.value - 80;
        mixer.SetFloat("MasterVol", newVol);
        GlobalVariables.MasterVolume = newVol;
    }
}
