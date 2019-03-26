using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{

    private GameObject[] players;
    private GameObject[] panels;
    private int[] scores;
    private int pCount;

    // Start is called before the first frame update
    void Start()
    {
        GameObject prefab = Resources.Load("PlayerPanel") as GameObject;
        players = GameObject.FindGameObjectsWithTag("Player");
        pCount = players.Length;
        //Debug.Log("pCount: " + pCount);
        scores = new int[pCount];
        panels = new GameObject[pCount];
        //Debug.Log("panels: " + panels);
        
        for (int i = 0; i < pCount; i++) {
            //Debug.Log(prefab);
            players[i] = players[i].transform.Find("UI").Find("Score").gameObject;
            panels[i] = Instantiate(prefab, transform);
            scores[i] = System.DateTime.Now.Millisecond;
            Debug.Log("START: " + scores[i]);
            panels[i].transform.localPosition = new Vector3(-135, 125 - 50 * i, 0);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < pCount; i++) {
            Score score = players[i].GetComponent<Score>();
            
            
            scores[i] = score.GetScore();
            Debug.Log("score: " + scores[i]);
            panels[i].transform.Find("Score").GetComponent<Text>().text = scores[i].ToString();
        }
    }
}

