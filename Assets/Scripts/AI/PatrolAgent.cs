using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class PatrolAgent : MonoBehaviour
    {

        private Transform targetPoint;
        private Transform[] patrolPoints;

        private Steering steering;

        private readonly float targetRadius = 10f;
        private bool init;

        private void OnEnable() {
            if (init) FindClosestPoint();
        }

        private void Awake() {
            steering = GetComponent<Steering>();
        }

        private void Start() {
            Transform parent = GameObject.Find("PatrolPoints").transform;
            patrolPoints = new Transform[parent.childCount];
            int i = 0;
            foreach (Transform t in parent) {
                patrolPoints[i] = t;
                i++;
            }
            FindClosestPoint();
            init = true;
        }

        void FixedUpdate() {
            if (Vector3.Magnitude(transform.position - targetPoint.transform.position) > targetRadius) {
                steering.Steer(steering.Arrive(targetPoint.transform.position));
            } else {
                GetNextPoint();
            }
        }

        private void FindClosestPoint() {
            float dist, minDist = Mathf.Infinity;
            foreach (Transform t in patrolPoints) {
                dist = Vector3.Magnitude(t.position - transform.position);
                if (dist < minDist) {
                    minDist = dist;
                    targetPoint = t;
                }
            }
        }

        private void GetNextPoint() {
            Transform[] adjacentPoints = targetPoint.GetComponent<PatrolPoint>().adjacentPoints;
            int select = Random.Range(0, adjacentPoints.Length);
            targetPoint = adjacentPoints[select];
        }
    }
}
