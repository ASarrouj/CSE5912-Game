using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;

    private int roundLength;

    // Start is called before the first frame update
    void Start()
    {
        roundLength = 180;
    }

    void Update()
    {
        string seconds = ((int)Mathf.Ceil(roundLength - Time.time) % 60).ToString();
        if (seconds.Length == 1) seconds = "0" + seconds;

        timerText.text = "Time: " + ((int)Mathf.Ceil(roundLength - Time.time) / 60).ToString() + ":" + seconds;
    }
}
