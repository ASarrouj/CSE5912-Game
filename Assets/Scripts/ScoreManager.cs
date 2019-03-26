using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreManager : NetworkBehaviour
{
    public int[] Score;

    private GameObject[] players;

    public delegate void OnMatchStart();
    public event OnMatchStart onMatchStart;

    static public ScoreManager singleton;

    private void Awake() {
        singleton = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level) {
        
        if (level > 0) {
            Invoke("MatchStart", 3);
        }
    }

    private void MatchStart() {
        players = GameObject.FindGameObjectsWithTag("Player");
        Score = new int[players.Length];
        for (int i = 0; i < players.Length; i++) {
            Score[i] = 0;
            if (isServer) GetPlayerIndices();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetPlayerIndices() {
        /*
        for (int i = 0; i < players.Length; i++) {
            if (NetworkManager.singleton.client.connection.playerControllers[0].gameObject == players[i]) {
                RpcAssignPlayerIndex(i);     
            }
        }
        */
    }

    [ClientRpc]
    private void RpcAssignPlayerIndex(int i) {  
        players[i].GetComponent<Score>().GetPlayerIndex(i);
    }

    public void UpdateScore(int index, int value) {
        if (isServer) {
            Score[index] = value;
        } /*else {
            CmdUpdateScore(index, value);
        } */
    }

    [Command]
    private void CmdUpdateScore(int index, int value) {
        Score[index] = value;
    }
}
