using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Hide : MonoBehaviour
    {
        public float distanceFromBoundary = 5f;
        public bool arriving = false;

        private Steering steering;

        private Vector3 currentSpot;

        void Awake()
        {
            steering = GetComponent<Steering>();
        }

        public Vector3 GetSteering(Rigidbody target, ICollection<Transform> obstacles) {
            Vector3 bestHidingSpot;
            return GetSteering(target, obstacles, out bestHidingSpot);
        }

        public Vector3 GetSteering(Rigidbody target, ICollection<Transform> obstacles, out Vector3 bestHidingSpot) {

            if (arriving) {
                bestHidingSpot = currentSpot;
            } else {
                float distToClosest = Mathf.Infinity;
                bestHidingSpot = Vector3.zero;
                foreach (Transform o in obstacles) {
                    Vector3 hidingSpot = GetHidingPosition(o, target);
                    float dist = Vector3.Distance(hidingSpot, transform.position);
                    if (dist < distToClosest) {
                        distToClosest = dist;
                        bestHidingSpot = hidingSpot;
                    }
                }
                currentSpot = bestHidingSpot;
            }

            return steering.Arrive(bestHidingSpot);
        }

        Vector3 GetHidingPosition(Transform obstacle, Rigidbody target) {
            BoxCollider col = obstacle.GetComponent<BoxCollider>();
            float distAway = Mathf.Max(obstacle.transform.localScale.x, obstacle.transform.localScale.z) 
                * Mathf.Max(col.size.x, col.size.z) + distanceFromBoundary;
            float altitude = transform.position.y;
            Vector3 tar = target.position;
            tar.y = altitude;
            Vector3 obst = obstacle.position;
            obst.y = altitude;
            Vector3 dir = obst - tar;
            dir.Normalize();
            Vector3 hidePos = obst + dir * distAway;
            return hidePos;
        }

    }

}
