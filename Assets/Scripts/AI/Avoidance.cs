using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    [RequireComponent(typeof(FOV))]
    public class Avoidance : MonoBehaviour
    {
        public bool AvoidLeft { get; set; }
        public bool AvoidRight { get; set; }
        public bool Stuck { get; set; }

        private DetectObstacle shortDetect, longDetect;

        void Start()
        {
            shortDetect = GetComponent<FOV>().shortFOV.AddComponent<DetectObstacle>();
            longDetect = GetComponent<FOV>().longFOV.AddComponent<DetectObstacle>();
        }

        void FixedUpdate()
        {
            AvoidLeft = false;
            AvoidRight = false;      
        }

    }
}
