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

        private readonly float fleeRadius = 10f;
        private readonly float hidingSpotRadius = 10f;

        private void Awake() {
            steering = GetComponent<Steering>();
            hide = GetComponent<Hide>();
            hidePosition = new Vector3(0, -100, 0);
        }

        void Start()
        {
            obstacles = GetObstacles();
        }

        void Update()
        {
            if (target == null) {
                target = GameObject.FindGameObjectWithTag("Player");
            }

            Vector3 dist = transform.position - target.transform.position;
            if (dist.magnitude < fleeRadius) {
                steering.Steer(dist); //flee
                hide.arriving = false;
            } else {
                Vector3 dir = hide.GetSteering(target.GetComponent<Rigidbody>(), obstacles, out hidePosition); //hide
                if (Vector3.Magnitude(transform.position - hidePosition) > hidingSpotRadius) {
                    hide.arriving = true;
                    steering.Steer(dir);
                } else if (hide.arriving) {
                    hide.arriving = false;
                    steering.Stop();
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
