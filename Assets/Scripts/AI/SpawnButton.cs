using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnButton : MonoBehaviour
{
    private AI_Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnerObj = GameObject.Find("AI_Spawner");
        if (spawnerObj != null) spawner = spawnerObj.GetComponent<AI_Spawner>();
    }

    public void SpawnAI() {
        if (spawner != null && transform.root.GetComponent<NetworkIdentity>().isServer) {
            spawner.SpawnAI();
        }
    }
}
