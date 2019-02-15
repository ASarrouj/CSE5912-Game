using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechCollisions : MonoBehaviour
{

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 13)
        {
            rb.freezeRotation = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 13)
        {
            rb.freezeRotation = false;
        }
    }
}
