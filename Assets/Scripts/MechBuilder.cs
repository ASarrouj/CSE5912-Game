using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechBuilder : MonoBehaviour
{

    public GameObject[] mechPrefabs;
    public GameObject[] partScreens;
    public GameObject mechScreen;

    public void moveToGunScreen(int screen)
    {
        mechScreen.SetActive(false);
        partScreens[screen].SetActive(true);
    }

    public void backToMechScreen(int current)
    {
        partScreens[current].SetActive(false);
        mechScreen.SetActive(true);
    }
}
