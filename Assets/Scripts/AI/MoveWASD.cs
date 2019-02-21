using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWASD : MonoBehaviour
{
    Rigidbody rb;
    float moveSpeed = 10f;
    float turnSpeed = 100f;
    Vector3 temp;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) {
            //temp 
            rb.velocity = rb.transform.forward * moveSpeed;
            //rb.velocity = new Vector3(temp.x * moveSpeed, rb.velocity.y, temp.z * moveSpeed);
        }

        if (Input.GetKey(KeyCode.S)) {
            //temp 
            rb.velocity = -rb.transform.forward * moveSpeed;
            //rb.velocity = new Vector3(temp.x * moveSpeed, rb.velocity.y, temp.z * moveSpeed);
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime, Space.World);
        }

    }
}
