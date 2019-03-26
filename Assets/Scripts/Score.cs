using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : NetworkBehaviour
{
    public Text scoreText;

    private int score;
    private int index;
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        //scoreText = gameObject.AddComponent<Text>();
        score = 0;
        UpdateScoreText();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void scoreUp(int value) 
    {
        score += value;
        scoreManager.UpdateScore(index, value);
        UpdateScoreText();
    }

    private void UpdateScoreText() {
        scoreText.text = "Score: " + score.ToString();
    }

    public void GetPlayerIndex(int i) {
        index = i;
    }

}
