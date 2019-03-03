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

        private Steering steering;
        private Combat combat;

        private float targetRadius = 20f;

        private void Awake() {
            follow = GetComponent<Follow>();
            steering = GetComponent<Steering>();
            combat = GetComponent<Combat>();
        }

        void FixedUpdate()
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
            combat.Attack(target);
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
