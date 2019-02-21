using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapToBroken : MonoBehaviour
{
    public GameObject standing;
    public GameObject broken;

    public void swapToBroken()
    {
        standing.SetActive(false);
        broken.SetActive(true);
    }


}
