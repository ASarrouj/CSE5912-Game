using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Prototype.NetworkLobby;

public class Timer : MonoBehaviour
{
    public Text timerText;

    public int roundLength;
    public float startTime;

    private RoundsManager rm;

    // Start is called before the first frame update
    void Start()
    {
        // get the start time and round length from the RoundsManager for the first round
        // RoundsManager sets them at the beginning of other rounds
        rm = GameObject.Find("RoundsManager").GetComponent<RoundsManager>();
        roundLength = rm.roundLengthSeconds;
        startTime = rm.roundStartTime;
    }

    void Update()
    {
        if (startTime == 0)
        {
            startTime = rm.roundStartTime;
        }

        string timeString = "";
        float endTime = startTime + roundLength;

        int seconds = ((int)Mathf.Ceil(endTime - Time.time) % 60);
        string secondsString = seconds.ToString();
        if (secondsString.Length == 1) secondsString = "0" + seconds;

        if (seconds < 0)
        {
            secondsString = "00";
        }

        timeString = "Time: " + ((int)Mathf.Ceil(endTime - Time.time) / 60).ToString() + ":" + secondsString;

        if (startTime - Time.time > 0)
        {
            timeString = "Time: 0:00";
            Debug.Log("unexpected overtime???");
            Debug.Log("start time: " + startTime);
            Debug.Log("end time: " + endTime);
        }

        timerText.text = timeString;
    }
}
