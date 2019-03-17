using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{ 
    public UnityEngine.Audio.AudioMixerGroup output;
    public Slider slider;

    private UnityEngine.Audio.AudioMixer mixer;
    private float newVol;

    // Start is called before the first frame update
    void Start()
    {
        mixer = output.audioMixer;
        slider.minValue = 0;
        slider.maxValue = 100;
        float vol;
        mixer.GetFloat(output.name, out vol);//GlobalVariables.MasterVolume + 80;
        slider.value = vol + 80;
    }

    // Update is called once per frame
    void Update()
    {
        newVol = slider.value - 80;
        mixer.SetFloat(output.name, newVol);
    }
}
