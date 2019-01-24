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
        speed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = (ball.position - transform.position).normalized;
        movement.x = 0.0f;
        movement.y = 0.0f;

        transform.Translate(movement * speed);
    }
}
