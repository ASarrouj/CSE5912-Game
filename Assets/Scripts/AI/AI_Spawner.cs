using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AI_Spawner : NetworkBehaviour
{
    public GameObject prefabAI;
    public bool debug = false;

    public void SpawnAI() {
        //NetworkManager man = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        //prefabAI = man.spawnPrefabs[0];

        GameObject newAI = Instantiate(prefabAI, new Vector3(-20, 0, 0), Quaternion.identity);
        if (debug) {
            newAI.GetComponent<AI.FOV>().showFOV = true;
            newAI.GetComponent<AI.Steering>().ShowDebugTarget = true;
            newAI.GetComponent<LineRenderer>().enabled = true;     
        }

        CmdSpawn(newAI);
    }

    [Command]
    private void CmdSpawn(GameObject g) {
        NetworkServer.Spawn(g);
    }
}
