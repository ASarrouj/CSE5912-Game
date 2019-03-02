using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AI_Spawner : MonoBehaviour
{
    public GameObject prefabAI;
    public bool debug = false;

    public void SpawnAI() {
        GameObject newAI = Instantiate(prefabAI, new Vector3(-20, 0, 0), Quaternion.identity);
        if (debug) {
            newAI.GetComponent<AI.FOV>().showFOV = true;
            newAI.GetComponent<AI.Steering>().ShowDebugTarget = true;
            newAI.GetComponent<LineRenderer>().enabled = true;     
        }
        NetworkServer.Spawn(newAI);
    }
}
