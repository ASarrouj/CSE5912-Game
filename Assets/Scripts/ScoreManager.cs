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

    // Start is called before the first frame update
    void Start()
    {
        name = "ScoreManager";
        DontDestroyOnLoad(gameObject);
        Invoke("MatchStart", 1);

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
        //SyncListIntScores[index] = value;
          SyncListIntScores.RemoveAt(index);
          SyncListIntScores.Insert(index, value);
        scoreboard.UpdateScores();
        RpcUpdateScore(index);
    }

    [Command]
    public void CmdUpdateScore(int index, int value) {
        Debug.Log("Cmd: " + index + ", " + value);
        Debug.Log("sync1: " + SyncListIntScores[index]);
        SyncListIntScores.RemoveAt(index);
        SyncListIntScores.Insert(index, value);
        SyncListIntScores.Dirty(index);
        
        Debug.Log("sync2: " + SyncListIntScores[index]);
    }

    [ClientRpc]
    private void RpcUpdateScore(int index) {//, int value) {
        Debug.Log("Rpc: " + index);// + ", " + value);
        Debug.Log("sync1: " + SyncListIntScores[index]);
        //SyncListIntScores[index] = value;
        SyncListIntScores.Dirty(index);
        scoreboard.UpdateScores();
        Debug.Log("sync2: " + SyncListIntScores[index]);
    }
}
