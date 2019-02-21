using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Hide : MonoBehaviour
    {
        public float distanceFromBoundary = 5f;
        Steering steering;
        
        void Awake()
        {
            steering = GetComponent<Steering>();
        }

        public Vector3 GetSteering(Rigidbody target, ICollection<Rigidbody> obstacles) {
            Vector3 bestHidingSpot;
            return GetSteering(target, obstacles, out bestHidingSpot);
        }

        public Vector3 GetSteering(Rigidbody target, ICollection<Rigidbody> obstacles, out Vector3 bestHidingSpot) {
            float distToClosest = Mathf.Infinity;
            bestHidingSpot = Vector3.zero;
            foreach (Rigidbody r in obstacles) {
                //Debug.Log("FLAG");
                Vector3 hidingSpot = GetHidingPosition(r, target);
                float dist = Vector3.Distance(hidingSpot, transform.position);
                if (dist < distToClosest) {
                    distToClosest = dist;
                    bestHidingSpot = hidingSpot;
                }
            }

            /*
            if (distToClosest == Mathf.Infinity) {
                Debug.Log("NO OBSTACLES");
                return Vector3.zero;
            }
            */
            //Debug.Log("BEST: " + bestHidingSpot);
            return steering.Arrive(bestHidingSpot);
        }

        Vector3 GetHidingPosition(Rigidbody obstacle, Rigidbody target) {
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
            //Debug.Log(target.position);
            return obst + dir * distAway;
        }

    }

}
