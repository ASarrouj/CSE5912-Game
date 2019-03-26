using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public ScoreManager scoreManager;

    private GameObject panelPrefab;
    private GameObject[] players;
    private GameObject[] panels;
    private int pCount;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        panelPrefab = Resources.Load("PlayerPanel") as GameObject;
        players = GameObject.FindGameObjectsWithTag("Player");
        pCount = players.Length;
        panels = new GameObject[pCount];
        
        for (int i = 0; i < pCount; i++) {
            panels[i] = Instantiate(panelPrefab, transform);
            panels[i].transform.localPosition = new Vector3(-135, 125 - 50 * i, 0);
            panels[i].transform.Find("PlayerName").GetComponent<Text>().text = players[i].name;
        }

    }

    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < pCount; i++) {
            panels[i].transform.Find("Score").GetComponent<Text>().text = scoreManager.Score[i].ToString();
        }
    }
}

