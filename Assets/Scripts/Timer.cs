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
        timerText.text = "Time: " + ((int)Mathf.Ceil(roundLength - Time.time) / 60).ToString() + ":" + ((int)Mathf.Ceil(roundLength - Time.time) % 60).ToString();
    }
}
