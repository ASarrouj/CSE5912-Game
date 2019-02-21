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
        private float angleThreshold = 1;
        private int maxSpeed = 12;

        Rigidbody rb;

        GameObject debugTar;

        Avoidance avoid;

        void Awake()
        {
            rb = gameObject.GetComponent<Rigidbody>();

            if (ShowDebugTarget) {
                debugTar = GameObject.CreatePrimitive(PrimitiveType.Cube);
                debugTar.GetComponent<Renderer>().material.color = Color.red;
                debugTar.name = "AI Target";
            }

            mechStats = GetComponent<MoveTest>();

            avoid = GetComponent<Avoidance>();
        }

        private bool CheckObstacle() {
            return Physics.Raycast(transform.position, transform.forward, 20f, 0, QueryTriggerInteraction.Ignore);
        }

        public void Steer(Vector3 linearAcceleration) {

            if (linearAcceleration.magnitude == 0) {
                mechStats.moveSpeed = 0;
                return;
            }

            //Debug.Log("_ACCEL " + rb.velocity);
            float angle = Vector3.SignedAngle(transform.forward, linearAcceleration, Vector3.up);
            //float angle =  transform.rotation.eulerAngles.y -
            //Debug.Log("ANGLE: " + angle);
            /* if (CheckObstacle()) {
                Debug.Log("BLOCKED");
                mechStats.moveSpeed = speedStep;
                mechStats.rotateSpeed = 30;
            } else */

            if (avoid.AvoidRight) {
                SteerRight();
            } else if (avoid.AvoidLeft) {
                SteerLeft();
            } else if (angle > angleThreshold) {
                SteerRight();
            } else if (angle < - angleThreshold) {
                SteerLeft();
            } else if (mechStats.moveSpeed < maxSpeed) {
                    mechStats.rotateSpeed = 0;
                    mechStats.moveSpeed += speedStep;
            }

            /*
            rb.velocity += linearAcceleration * Time.deltaTime;
            if (rb.velocity.magnitude > maxVelocity) {
                rb.velocity = rb.velocity.normalized * maxVelocity;
            }
            */
        }

        private void SteerRight() {
            if (mechStats.rotateSpeed < 30) {
                mechStats.moveSpeed = speedStep;
                mechStats.rotateSpeed += rotateStep;
                //Debug.Log("R");
            }
        }

        private void SteerLeft() {
            if (mechStats.rotateSpeed > -30) {
                mechStats.moveSpeed = speedStep;
                mechStats.rotateSpeed -= rotateStep;
                //Debug.Log("L");
            }
        }

        

        public Vector3 Arrive(Vector3 targetposition) {
            if (ShowDebugTarget) debugTar.transform.position = targetposition;
            //Debug.Log("TAR: " + targetposition);
            Vector3 targetVelocity = targetposition - rb.position;

            float dist = targetVelocity.magnitude;
            
            if (dist < targetRadius) {
                Debug.Log("CLOSE");
                return Vector3.zero;
            }
            

            /*
            float targetSpeed;
            
            if (dist > slowRadius) {
                targetSpeed = maxVelocity;
            } else {
                targetSpeed = maxVelocity * (dist / slowRadius);
            }*/

            /*
            targetSpeed = maxVelocity;

            targetVelocity.Normalize();
            targetVelocity *= targetSpeed;

            Vector3 acceleration = targetVelocity - rb.velocity;
            acceleration *= 1 / timeToTarget;

            if (acceleration.magnitude > maxAcceleration) {
                acceleration.Normalize();
                acceleration *= maxAcceleration;
            }
            */

            return targetVelocity; //acceleration;
        }
    }
}
