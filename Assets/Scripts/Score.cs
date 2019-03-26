using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void scoreUp(int value) 
    {
        score += value;
        scoreText.text = "Score: " + score.ToString();
    }

    public int GetScore() {
        return score;
    }

}
