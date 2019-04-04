using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreSpawner : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CmdSpawnSM();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Command]
    private void CmdSpawnSM() {
        GameObject.Find("LobbyManager").GetComponent<LobbyManager>().CreateScoreManager();
    }
}
