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

    public Transform[] SpawnPoints;

    /*
    private Vector3[] citySpawnPoints = {
        new Vector3(-3.2f, 0, -16.1f),
        new Vector3(-16.5f, 0, -34.6f),
        new Vector3(-3.2f, 0, -34.5f),
        new Vector3(-15.5f, 0, -16.1f)};

    private Vector3[] moonSpawnPoints = {
        new Vector3(990, 300, 990),
        new Vector3(990, 300, 1010),
        new Vector3(1010, 300, 1010),
        new Vector3(1010, 300, 990)};
        */

    [Header("Debug")]
    public bool ShowFOV = false;
    public bool ShowTarget = false;
    public bool ShowPath = false;

    public void Start() {
        DontDestroyOnLoad(gameObject);
    }

    public void SpawnAI(int startPos) {
        //NetworkManager man = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        //prefabAI = man.spawnPrefabs[0];

        GameObject newAI = Instantiate(prefabAI, SpawnPoints[startPos].position, Quaternion.identity);

        if (ShowFOV) newAI.GetComponent<AI.FOV>().showFOV = true;
        if (ShowTarget) newAI.GetComponent<AI.Steering>().ShowDebugTarget = true;
        if (ShowPath) newAI.GetComponent<LineRenderer>().enabled = true;     

        CmdSpawn(newAI);
    }

    /*
    public void SetSpawnPoints(int sceneNum) {
        if (sceneNum == 1) {
            SpawnPoints = citySpawnPoints;
        } else if (sceneNum == 2) {
            SpawnPoints = moonSpawnPoints;
        }
    }
    */

    private void GetSpawnPoints() {
        Transform parent = GameObject.Find("PatrolPoints").transform;
        SpawnPoints = new Transform[parent.childCount];
        int i = 0;
        foreach (Transform t in parent) {
            SpawnPoints[i] = t;
            i++;
        }
    }

    [Command]
    private void CmdSpawn(GameObject g) {
        NetworkServer.Spawn(g);
    }

    private void Spawn(GameObject g) {
        NetworkServer.Spawn(g);
    }

    public void AddStartAI(int num) {
        numStartAI = num;
    }

    private void OnLevelWasLoaded() {
        GetSpawnPoints();
        Invoke("SpawnStartAI", 1);
    }

    private void SpawnStartAI() {
        for (int i = 0; i < numStartAI; i++) {
            SpawnAI(i);
        }
    }
}
