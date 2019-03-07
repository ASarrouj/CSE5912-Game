using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AI_Spawner : NetworkBehaviour
{
    public GameObject prefabAI;
    public int numStartAI;

    [Header("Debug")]
    public bool ShowFOV = false;
    public bool ShowTarget = false;
    public bool ShowPath = false;

    public void Start() {
        DontDestroyOnLoad(gameObject);
    }

    public void SpawnAI(Vector3 startPos) {
        //NetworkManager man = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        //prefabAI = man.spawnPrefabs[0];

        GameObject newAI = Instantiate(prefabAI, startPos, Quaternion.identity);

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

    public void AddStartAI(InputField num) {
        numStartAI = int.Parse(num.text);
    }

    private void OnLevelWasLoaded(int level) {
        if (level == 1) {
            for (int i = 0; i < numStartAI; i++) {
                SpawnAI(new Vector3(-50 - i * 10, 0, -20 - i * 10));
            }
        }
    }
}
