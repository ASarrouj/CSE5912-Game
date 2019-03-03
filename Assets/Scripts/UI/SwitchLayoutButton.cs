using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLayoutButton : MonoBehaviour
{
    public GameObject currentButtons;
    public GameObject nextButtons;

    public void SwitchLayout()
    {
        currentButtons.SetActive(false);
        nextButtons.SetActive(true);
    }
}
