using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public UnityEngine.Audio.AudioMixer mixer;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.minValue = 0;
        slider.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        mixer.SetFloat("MasterVol", slider.value - 80);
    }
}
