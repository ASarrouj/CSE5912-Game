using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAIScript : MonoBehaviour
{
    private Transform ball;
    private float speed;
    private bool pause;
    public Light spot;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Sphere").transform;
        speed = 0.25f;
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {        
        spot.transform.position=transform.position+new Vector3(0,10f,0);
        if (!pause)
        {
            Vector3 movement = (ball.position - transform.position).normalized;
            movement.x = 0.0f;
            movement.y = 0.0f;

            transform.Translate(movement * speed);
            CheckInput();
        }
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = new Vector3(9, 1, 0);
        }
    }

    public void Halt()
    {
        pause = true;
    }

    public void Resume()
    {
        pause = false;
    }
}
