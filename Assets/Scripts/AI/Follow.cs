using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Follow : MonoBehaviour
    {
        private Steering steering;
        
        void Awake()
        {
            steering = GetComponent<Steering>();
        }

        public Vector3 GetSteering(Rigidbody target) {
            Vector3 targetLocation;
            return GetSteering(target, out targetLocation);
        }

        public Vector3 GetSteering(Rigidbody target, out Vector3 targetLocation) {
            targetLocation = target.position;
            return steering.Arrive(targetLocation);
        }

    }

}
