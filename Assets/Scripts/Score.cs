using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Prototype.NetworkLobby;

public class Score : NetworkBehaviour
{
    public Text scoreText;
    public int scoreID; 

    private int score;
    private ScoreManager scoreManager;
    private PlayerStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer) CmdSpawnSM();
        score = 0;
        UpdateScoreText(0);       
        playerStatus = transform.root.GetComponent<PlayerStatus>();
        scoreID = playerStatus.index;
        Invoke("MatchStart", 4);
    }

    // Update is called once per frame
    void MatchStart()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        if (!isLocalPlayer) transform.GetComponent<Score>().enabled = false;
    }

    public void ScoreUp(int value) 
    {
        if (!isLocalPlayer) return;
        score += value;
        scoreManager.AddScore(scoreID, value);
    }

    [Command]
    private void CmdUpdateScore(int i, int sc) {
        NetworkIdentity smID = scoreManager.GetComponent<NetworkIdentity>();
        smID.AssignClientAuthority(connectionToClient);
        scoreManager.UpdateScore(i, sc);
        smID.RemoveClientAuthority(connectionToClient);
    }

    public void UpdateScoreText(int sc) {
        score = sc;
        scoreText.text = "Score: " + score.ToString();
    }

    [Command]
    private void CmdSpawnSM() {
        GameObject.Find("LobbyManager").GetComponent<LobbyManager>().CreateScoreManager();
    }

}
