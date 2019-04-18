using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class Steering : MonoBehaviour
    {
        public bool ShowDebugTarget = false;

        private MechDriver mechDriver;
        private readonly int rotateStep = 10;
        private readonly int speedStep = 4;
        private readonly float angleThreshold = 10;
        private readonly int maxSpeed = 12;
        private readonly int maxAngle = 45;

        private int turnCounter = 0;
        private int turnWait = 50;
        private bool backup = false;
        private bool turning = false;

        Rigidbody rb;

        GameObject debugTar;

        Avoidance avoid;

        void Awake()
        {
            rb = gameObject.GetComponent<Rigidbody>();
            mechDriver = GetComponent<MechDriver>();
            avoid = GetComponent<Avoidance>();;      
        }

        void Start() {
            GetComponent<MechDriver>().findColliders();
            if (ShowDebugTarget) {
                debugTar = GameObject.CreatePrimitive(PrimitiveType.Cube);
                debugTar.GetComponent<Renderer>().material.color = Color.red;
                Destroy(debugTar.GetComponent<BoxCollider>());
                debugTar.name = "AI Target";
            }
        }

        public void Steer(Vector3 linearAcceleration) {

            if (backup) return;

            if (linearAcceleration.magnitude == 0) {
                Stop();
                return;
            }           

            

            float angle = Vector3.SignedAngle(transform.forward, linearAcceleration, Vector3.up);

            if (avoid.AvoidRight) {
                if (turning) SteerRight();
                else BackUp();
            } else if (avoid.AvoidLeft) {
                if(turning) SteerLeft();
                else BackUp();
            } else if (angle > angleThreshold) {
                SteerRight();
            } else if (angle < - angleThreshold) {
                SteerLeft();
            } else {
                mechDriver.turnAngle = 0;
                //mechDriver.Accelerate();
                //if (mechDriver.velLimit < maxSpeed) {
                //mechDriver.velLimit += speedStep;
                //}
            }
            mechDriver.Accelerate();
            
        }

        private void BackUp() {
            backup = true;
            mechDriver.turnAngle = 0;
            mechDriver.velLimit = -4 * speedStep;
            Invoke("DoneBackingUp", 0.5f);
        }

        private void DoneBackingUp() {
            backup = false;
            turning = true;
            Invoke("DoneTurning", 0.5f);
        }

        private void DoneTurning() {
            avoid.AvoidLeft = false;
            avoid.AvoidRight = false;
            turning = false;
        }

        private void SteerRight() {
            //if (++turnCounter < turnWait) return;
            //if (mechDriver.turnAngle < maxAngle) {
                //mechDriver.velLimit = speedStep;
               // mechDriver.turnAngle += rotateStep;
          //  }
            //turnCounter = 0;
            mechDriver.velLimit = 2 * speedStep;
            //mechDriver.Decelerate();
            mechDriver.TurnRight();
        }

        private void SteerLeft() {
            if (++turnCounter < turnWait) return;
            //if (mechDriver.turnAngle > -maxAngle) {
               // mechDriver.velLimit = speedStep;
              //  mechDriver.turnAngle -= rotateStep;
            //}
            //turnCounter = 0;
            mechDriver.velLimit = 2 * speedStep;
            //mechDriver.Decelerate();
            mechDriver.TurnLeft();
        }

        public void Stop() {
            mechDriver.velLimit = 0;
            //if (mechDriver.velLimit > 1) mechDriver.Decelerate();
            mechDriver.turnAngle = 0;
        }   

        public Vector3 Arrive(Vector3 targetposition) {
            if (ShowDebugTarget) debugTar.transform.position = targetposition;
            Vector3 targetVelocity = targetposition - rb.position;

            float dist = targetVelocity.magnitude;

            return targetVelocity;
        }
    }
}
