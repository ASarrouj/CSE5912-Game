using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AI_Spawner : NetworkBehaviour
{
    public GameObject prefabAI;

    [Header("Debug")]
    public bool ShowFOV = false;
    public bool ShowTarget = false;
    public bool ShowPath = false;

    private bool conn = false;

    private GameObject[] players;

    public void SpawnAI() {
        //NetworkManager man = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        //prefabAI = man.spawnPrefabs[0];

        GameObject newAI = Instantiate(prefabAI, new Vector3(-50, 0, -20), Quaternion.identity);

        if (ShowFOV) newAI.GetComponent<AI.FOV>().showFOV = true;
        if (ShowTarget) newAI.GetComponent<AI.Steering>().ShowDebugTarget = true;
        if (ShowPath) newAI.GetComponent<LineRenderer>().enabled = true;     

        CmdSpawn(newAI);
    }

    [Command]
    private void CmdSpawn(GameObject g) {
        NetworkServer.Spawn(g);
    }

    private void Spawn(GameObject g) {
        NetworkServer.Spawn(g);
    }
}
