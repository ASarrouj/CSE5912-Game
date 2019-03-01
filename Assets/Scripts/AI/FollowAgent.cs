using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class FollowAgent : MonoBehaviour
    {
        
        public GameObject target;

        private GameObject[] enemies;
        private Follow follow;

        private Rigidbody rb;
        private Steering steering;

        private float targetRadius = 20f;

        private void Awake() {
            follow = GetComponent<Follow>();
            steering = GetComponent<Steering>();
        }

        void Start()
        {
            rb = GetComponent<Rigidbody>();     
        }

        void Update()
        {
            if (target == null) {
                TargetClosestEnemy();
            }
            else if (Vector3.Magnitude(transform.position - target.transform.position) > targetRadius) {
                Vector3 followAccel = follow.GetSteering(target.GetComponent<Rigidbody>());
                steering.Steer(followAccel);
            } else {
                steering.Stop();
            }
        }

        private void TargetClosestEnemy() {
            enemies = GameObject.FindGameObjectsWithTag("Player");
            float distToClosest = Mathf.Infinity;
            foreach (GameObject g in enemies) {
                Vector3 enemyLoc = g.transform.position;
                float dist = Vector3.Distance(enemyLoc, transform.position);
                if (dist < distToClosest) {
                    distToClosest = dist;
                    target = g;
                }
            }
        }

        public void OnDisable() {
            
        }
    }
}
