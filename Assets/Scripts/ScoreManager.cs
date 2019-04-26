using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ScoreManager : NetworkBehaviour
{
    public SyncListInt SyncListIntScores = new SyncListInt();

    private GameObject[] players;
    public Scoreboard scoreboard;

    public delegate void OnMatchStart();
    public event OnMatchStart onMatchStart;

    private NetworkIdentity networkIdentity;

    // Start is called before the first frame update
    void Start()
    {
        name = "ScoreManager";
        DontDestroyOnLoad(gameObject);
        Invoke("MatchStart", 1);
        networkIdentity = GetComponent<NetworkIdentity>();
    }

    void Awake() {
        SyncListIntScores.Callback = Test;
    }

    void Test(SyncList<int>.Operation op, int itemIndex) {
        Debug.Log("Score " + itemIndex + " changed: " + op);
    }

    private void MatchStart() {       
        players = GameObject.FindGameObjectsWithTag("Player");
        if (isServer) {
            for (int i = 0; i < players.Length; i++) {
                SyncListIntScores.Add(0);
            }
        }
    }

    public void UpdateScore(int index, int value) {
        SyncListIntScores[index] = value;
        scoreboard.UpdateScore(index, value);
        RpcUpdateScoreboard(index, value);
    }

    public void AddScore(int index, int value) {
        UpdateScore(index, SyncListIntScores[index] + value);
    }

    [ClientRpc]
    private void RpcUpdateScoreboard(int index, int value) {
        scoreboard.UpdateScore(index, value);
    }
}
