using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    [RequireComponent(typeof(FOV))]
    public class Avoidance : MonoBehaviour
    {
        public bool AvoidLeft { get; set; }
        public bool AvoidRight { get; set; }

        void Start()
        {
            GetComponent<FOV>().fov.AddComponent<DetectObstacle>();
        }

        void FixedUpdate()
        {
            //AvoidLeft = false;
            //AvoidRight = false;      
        }

    }
}
