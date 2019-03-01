using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class Steering : MonoBehaviour
    {
        public bool ShowDebugTarget = false;

        private MoveTest mechStats;
        private readonly int rotateStep = 5;
        private readonly int speedStep = 4;
        private readonly float angleThreshold = 10;
        private readonly int maxSpeed = 12;
        private readonly int maxAngle = 30;
        private readonly float targetRadius = 20f;
        private Vector3 targetPos;

        Rigidbody rb;

        GameObject debugTar;

        Avoidance avoid;
        Hide hide;

        void Awake()
        {
            rb = gameObject.GetComponent<Rigidbody>();
            mechStats = GetComponent<MoveTest>();
            avoid = GetComponent<Avoidance>();
            hide = GetComponent<Hide>();

            if (ShowDebugTarget) {
                debugTar = GameObject.CreatePrimitive(PrimitiveType.Cube);
                debugTar.GetComponent<Renderer>().material.color = Color.red;
                Destroy(debugTar.GetComponent<BoxCollider>());
                debugTar.name = "AI Target";
            }           
        }

        public void Steer(Vector3 linearAcceleration) {

            if (linearAcceleration.magnitude == 0) {
                Stop();
                return;
            }           

            float angle = Vector3.SignedAngle(transform.forward, linearAcceleration, Vector3.up);

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
            if (mechStats.rotateSpeed < maxAngle) {
                mechStats.moveSpeed = speedStep;
                mechStats.rotateSpeed += rotateStep;
            }
        }

        private void SteerLeft() {
            if (mechStats.rotateSpeed > - maxAngle) {
                mechStats.moveSpeed = speedStep;
                mechStats.rotateSpeed -= rotateStep;
            }
        }

        public void Stop() {
            mechStats.moveSpeed = 0;
            mechStats.rotateSpeed = 0;
        }   

        public Vector3 Arrive(Vector3 targetposition) {
            targetPos = targetposition;
            if (ShowDebugTarget) debugTar.transform.position = targetposition;
            Vector3 targetVelocity = targetposition - rb.position;

            float dist = targetVelocity.magnitude;

            return targetVelocity;
        }
    }
}
