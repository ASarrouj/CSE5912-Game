using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class Steering : MonoBehaviour
    {
        [SerializeField]
        bool ShowDebugTarget = true;

        public float targetRadius = 1f;
        public float slowRadius = 0f;
        public float timeToTarget = 0.1f;

        private MoveTest mechStats;
        private int rotateStep = 5;
        private int speedStep = 4;
        private float angleThreshold = 10;
        private int maxSpeed = 12;
        private bool stop = false;

        Rigidbody rb;

        GameObject debugTar;

        Avoidance avoid;

        void Awake()
        {
            rb = gameObject.GetComponent<Rigidbody>();
            mechStats = GetComponent<MoveTest>();
            avoid = GetComponent<Avoidance>();

            if (ShowDebugTarget) {
                debugTar = GameObject.CreatePrimitive(PrimitiveType.Cube);
                debugTar.GetComponent<Renderer>().material.color = Color.red;
                Destroy(debugTar.GetComponent<BoxCollider>());
                debugTar.name = "AI Target";
            }           
        }

        private bool CheckObstacle() {
            return Physics.Raycast(transform.position, transform.forward, 20f, 0, QueryTriggerInteraction.Ignore);
        }

        public void Steer(Vector3 linearAcceleration) {

            if (linearAcceleration.magnitude == 0) {
                mechStats.moveSpeed = 0;
                return;
            }

            float angle = Vector3.SignedAngle(transform.forward, linearAcceleration, Vector3.up);

            if (Vector3.Magnitude(debugTar.transform.position - transform.position) < 1) {
                Stop();
                return;
            }

            if (avoid.AvoidRight) {
                SteerLeft();
            } else if (avoid.AvoidLeft) {
                SteerRight();
            } else if (angle > angleThreshold) {
                SteerRight();
            } else if (angle < - angleThreshold) {
                SteerLeft();
            } else {
                mechStats.rotateSpeed = 0;
                if (mechStats.moveSpeed < maxSpeed) {
                    mechStats.moveSpeed += speedStep;
                }
            }
        }

        private void SteerRight() {
            if (mechStats.rotateSpeed < 30) {
                mechStats.moveSpeed = speedStep;
                mechStats.rotateSpeed += rotateStep;
            }
        }

        private void SteerLeft() {
            if (mechStats.rotateSpeed > -30) {
                mechStats.moveSpeed = speedStep;
                mechStats.rotateSpeed -= rotateStep;
            }
        }

        public void Stop() {
            mechStats.moveSpeed = 0;
        }   

        public Vector3 Arrive(Vector3 targetposition) {
            if (ShowDebugTarget) debugTar.transform.position = targetposition;
            Vector3 targetVelocity = targetposition - rb.position;

            float dist = targetVelocity.magnitude;
            
            if (dist < targetRadius) {
                return Vector3.zero;
            }

            return targetVelocity;
        }
    }
}
