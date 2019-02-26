using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class HideAgent : MonoBehaviour
    {
        
        public GameObject target;

        private List<Transform> obstacles;
        private Vector3 hidePosition;
        private Steering steering;
        private Hide hide;

        private readonly float minHideDist = 40f;


        private void Awake() {
            steering = GetComponent<Steering>();
            hide = GetComponent<Hide>();  
        }

        void Start()
        {
            obstacles = GetObstacles();
        }

        void Update()
        {
            if (target == null) {
                target = GameObject.FindGameObjectWithTag("Player");
            } else {
                Vector3 dist = transform.position - target.transform.position;
                if (dist.magnitude < minHideDist) {
                    steering.Steer(dist); //flee
                } else {
                    steering.Steer(hide.GetSteering(target.GetComponent<Rigidbody>(), obstacles)); //hide
                }
            }
        }

        private List<Transform> GetObstacles() {
            List<Transform> obst = new List<Transform>();
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Building");
            foreach (GameObject g in obstacles) {
                Transform t = g.transform;
                if (g != null) {
                    obst.Add(t);
                }
            }
            return obst;
        }

        private void OnDisable() {
            
        }
    }
}
