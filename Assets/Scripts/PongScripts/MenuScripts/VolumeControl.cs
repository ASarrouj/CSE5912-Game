using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{ 
    public UnityEngine.Audio.AudioMixerGroup output;
    public Slider slider;

    private UnityEngine.Audio.AudioMixer mixer;
    private float startVol, newVol;

    // Start is called before the first frame update
    void OnEnable()
    {
        mixer = output.audioMixer;

        if (PlayerPrefs.HasKey(output.name)) {
            startVol = PlayerPrefs.GetFloat(output.name);
            mixer.SetFloat(output.name, startVol);
        } else {
            mixer.GetFloat(output.name, out startVol);
        }
        
        slider.minValue = 0;
        slider.maxValue = 100;
        slider.value = startVol + 80;
    }

    // Update is called once per frame
    void Update()
    {
        newVol = slider.value - 80;
        mixer.SetFloat(output.name, newVol);
        PlayerPrefs.SetFloat(output.name, newVol);
    }
}
