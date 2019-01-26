using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadState : MonoBehaviour
{
    GameObject player_paddle, AI_paddle, ball;
    Vector3 ball_vel, ball_pos, player_pos, AI_pos;
    int player_score, AI_score;

    bool anything_saved;

    // Start is called before the first frame update
    void Start()
    {
        anything_saved = false;
        player_paddle = GameObject.Find("Player1");
        AI_paddle = GameObject.Find("Player2");
        ball = GameObject.Find("Sphere");
    }

    // Update is called once per frame
    void Update()
    {
        // if we press the '1!' key, save the current info for each thing
        if (Input.GetKey(KeyCode.Alpha1))
        {
            player_score = ball.GetComponent<BallScript>().score1;
            AI_score = ball.GetComponent<BallScript>().score2;

            ball_vel = ball.GetComponent<BallScript>().vel;
            ball_pos = ball.transform.position;

            player_pos = player_paddle.transform.position;
            AI_pos = AI_paddle.transform.position;

            anything_saved = true;
        }

        // if we press the '2@' key, push the saved info for each thing
        if (anything_saved && Input.GetKey(KeyCode.Alpha2))
        {
            ball.GetComponent<BallScript>().score1 = player_score;
            AI_score = ball.GetComponent<BallScript>().score2 = AI_score;

            ball.GetComponent<BallScript>().vel = ball_vel;
            ball.transform.position = ball_pos;

            player_paddle.transform.position = player_pos;
            AI_paddle.transform.position = AI_pos;

            ball.GetComponent<BallScript>().updateScoreTexts();
        }
    }
}
