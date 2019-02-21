using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class HideAgent : MonoBehaviour
    {
        
        public GameObject target;

        private List<Rigidbody> obstacleRBs;
        private Vector3 hidePosition;
        private Steering steering;
        private Hide hide;


        private void Awake() {
            steering = GetComponent<Steering>();
            hide = GetComponent<Hide>();  
        }

        void Start()
        {
            obstacleRBs = GetObstacles();
        }

        void Update()
        {
            if (target == null) {
                target = GameObject.FindGameObjectWithTag("Player");
            } else {
                Vector3 hideAccel = hide.GetSteering(target.GetComponent<Rigidbody>(), obstacleRBs);
                steering.Steer(hideAccel);
            }
        }

        private List<Rigidbody> GetObstacles() {
            List<Rigidbody> obstRB = new List<Rigidbody>();
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Building");
            foreach (GameObject g in obstacles) {
                Rigidbody r = g.GetComponent<Rigidbody>();
                if (g != null) {
                    obstRB.Add(r);
                }
            }
            return obstRB;
        }

        private void OnDisable() {
            
        }
    }
}
