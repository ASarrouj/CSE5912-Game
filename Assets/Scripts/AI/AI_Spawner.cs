using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Spawner : MonoBehaviour
{
    public GameObject prefabAI;
    public bool debug = false;

    public void SpawnAI() {
        GameObject newAI = Instantiate(prefabAI, Vector3.zero, Quaternion.identity);
        if (debug) {
            newAI.GetComponent<AI.FOV>().showFOV = true;
            newAI.GetComponent<AI.Steering>().ShowDebugTarget = true;
            newAI.GetComponent<LineRenderer>().enabled = true;     
        }
    }
}
