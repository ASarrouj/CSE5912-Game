using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    void Update()
    {
        GUI.Box(new Rect(Screen.width / 2 - 5, Screen.height / 2, 10, 10), "");
    }
}
