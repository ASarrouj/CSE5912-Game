using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FOV))]
public class Avoidance : MonoBehaviour
{
    public bool AvoidLeft { get; set; }
    public bool AvoidRight { get; set; }

    void Start()
    {
        GetComponent<FOV>().fov.AddComponent<DetectObstacle>();
    }

    void Update()
    {
        AvoidLeft = false;
        AvoidRight = false;      
    }

}
