using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleAIScript : MonoBehaviour
{
    private Transform ball;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Sphere").transform;
        speed = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = (ball.position - transform.position).normalized;
        movement.x = 0.0f;
        movement.y = 0.0f;

        transform.Translate(movement * speed);
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = new Vector3(9, 1, 0);
        }
    }
}
