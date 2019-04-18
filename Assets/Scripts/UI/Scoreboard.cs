using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    private ScoreManager scoreManager;
    private GameObject panelPrefab;
    private GameObject[] players;
    private GameObject[] panels;
    private Text[] scoreText;
    private int pCount;

    void Start() {
        Invoke("MatchStart", 2);
    }

    void MatchStart()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        panelPrefab = Resources.Load("PlayerPanel") as GameObject;
        players = GameObject.FindGameObjectsWithTag("Player");
        pCount = players.Length;
        panels = new GameObject[pCount];
        scoreText = new Text[pCount];

        //Debug.Log(pCount);

        for (int i = 0; i < pCount; i++) {
            panels[i] = Instantiate(panelPrefab, transform);
            panels[i].transform.localPosition = new Vector3(-135, 125 - 50 * i, 0);
            panels[i].transform.Find("PlayerName").GetComponent<Text>().text = players[i].GetComponent<Prototype.NetworkLobby.PlayerStatus>().playerName;
            scoreText[i] = panels[i].transform.Find("Score").GetComponent<Text>();
            scoreText[i].text = "0";
        }

        scoreManager.scoreboard = this;
    }

    public void UpdateScore(int i, int sc) {
        scoreText[i].text = sc.ToString();
    }
}

