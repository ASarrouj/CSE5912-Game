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
    private Scoreboard scoreboard;
    private PlayerStatus playerStatus;

    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer) {
            //transform.GetComponent<Score>().enabled = false;
           // return;
        }
        Transform ui = transform.Find("UI");
        Debug.Log(ui);
        Transform hud = ui.Find("ScoreHUD");
        Debug.Log(hud);
        scoreboard = hud.GetComponent<Scoreboard>();
        Debug.Log(scoreboard);
        //scoreboard = transform.Find("UI").Find("ScoreHUD").GetComponent<Scoreboard>();
        //scoreText = gameObject.AddComponent<Text>();
        score = 0;
        UpdateScoreText();
        
        playerStatus = transform.root.GetComponent<PlayerStatus>();
        Invoke("MatchStart", 4);
    }

    // Update is called once per frame
    void MatchStart()
    {
        Debug.Log("???????????");
        
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        //ScoreUp(7);
        if (!isLocalPlayer) transform.GetComponent<Score>().enabled = false;
    }
 
    public void ScoreUp(int value) 
    {
        if (!isLocalPlayer) return;
        score += value;
        Invoke("UpdateScore", 1);
    }

    private void UpdateScore() {
        if (isLocalPlayer) {
            CmdUpdateScore(playerStatus.index, score);
        } else {
            Debug.Log("NOT LOCAL");
        }
        UpdateScoreText();

        /*
        if (isServer) {
            scoreManager.UpdateScore(playerStatus.index, score);
            //RpcUpdateScore(playerStatus.index, score);
        } else  {
            CmdUpdateScore(playerStatus.index, score);
        }
        //scoreManager.SyncListIntScores.Dirty(playerStatus.index);
        */
    }

    [Command]
    private void CmdUpdateScore(int i, int sc) {
        NetworkIdentity smID = scoreManager.GetComponent<NetworkIdentity>();
        smID.AssignClientAuthority(connectionToClient);
        scoreManager.UpdateScore(i, sc);
        smID.RemoveClientAuthority(connectionToClient);
        //RpcUpdateScore(i, sc);
        Debug.Log("C");
    }

    [ClientRpc]
    private void RpcUpdateScore(int index) {
        scoreManager.SyncListIntScores.Dirty(index);
        scoreboard.UpdateScores();
    }


    /*
    [ClientRpc]
    private void RpcUpdateScore(int i, int sc) {
        scoreManager.UpdateScore(i, sc);
        Debug.Log("R");
    }
    */

    private void UpdateScoreText() {
        scoreText.text = "Score: " + score.ToString();
    }

}
