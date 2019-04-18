using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class PlayerTargeting : MonoBehaviour
    {
        private GameObject[] players;
        private int updateCount = 0;

        public GameObject Target { get; set; }
        public float detectionRadius = 200f;


        // Start is called before the first frame update
        void Start()
        {
            GetPlayers();
            FindClosestPlayer();
        }

        // Update is called once per frame
        void Update()
        {
            updateCount++;
            if (updateCount > 50) {
                GetPlayers();
                FindClosestPlayer();
                updateCount = 0;
            }
        }

        public float TargetAngle() {
            Vector3 tarPos = transform.InverseTransformPoint(Target.transform.position);
            return Mathf.Atan2(tarPos.x, tarPos.z) * (180 / Mathf.PI);
        }

        private void GetPlayers() {
            players = GameObject.FindGameObjectsWithTag("Player");
        }

        private void FindClosestPlayer() {
            GameObject closestPlayer = null;
            float dist, minDist = Mathf.Infinity;
            foreach (GameObject p in players) {
                dist = Vector3.Magnitude(p.transform.position - transform.position);
                if (dist < minDist) {
                    minDist = dist;
                    closestPlayer = p;
                }
            }
            if (minDist < detectionRadius) {
                Target = closestPlayer;
            } else {
                Target = null;
            }
        
        }
    }
}
