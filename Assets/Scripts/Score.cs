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
        score = 0;
        UpdateScoreText();       
        playerStatus = transform.root.GetComponent<PlayerStatus>();
        Invoke("MatchStart", 2);
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
        Invoke("UpdateScore", 1);
    }

    private void UpdateScore() {
        if (isLocalPlayer) CmdUpdateScore(playerStatus.index, score);
        UpdateScoreText();
    }

    [Command]
    private void CmdUpdateScore(int i, int sc) {
        NetworkIdentity smID = scoreManager.GetComponent<NetworkIdentity>();
        smID.AssignClientAuthority(connectionToClient);
        scoreManager.UpdateScore(i, sc);
        smID.RemoveClientAuthority(connectionToClient);
    }

    private void UpdateScoreText() {
        scoreText.text = "Score: " + score.ToString();
    }

}
