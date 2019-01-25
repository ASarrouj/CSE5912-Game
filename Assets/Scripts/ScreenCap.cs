﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCap : MonoBehaviour
{
    private int count;
    private string path;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        path = "Screenshots/" + System.DateTime.Now.Ticks + "/";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Screenshot")) {
            if (count == 0) {
                System.IO.Directory.CreateDirectory(path);
            }
            count++;
            ScreenCapture.CaptureScreenshot(path + "screenshot" + count + ".png");
        }
    }
}
