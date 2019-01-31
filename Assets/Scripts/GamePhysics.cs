using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePhysics
{
    public static Vector3 gravity = new Vector3(0.0f, -0.1f, 0.0f);
    private static Rigidbody rb;

    public static void ApplyGravity(GameObject gameObject)
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(gravity);
    }
}