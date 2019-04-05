using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class FollowAgent : MonoBehaviour
    {
        
        private GameObject target;

        private GameObject[] enemies;
        private Follow follow;

        private Steering steering;
        private Combat combat;
        private PlayerTargeting targeting;

        private readonly float targetRadius = 10f;

        private void Awake() {
            follow = GetComponent<Follow>();
            steering = GetComponent<Steering>();
            combat = GetComponent<Combat>();
            targeting = GetComponent<PlayerTargeting>();
        }

        void FixedUpdate()
        {
            target = targeting.Target;
            if (Vector3.Magnitude(transform.position - target.transform.position) > targetRadius) {
                Vector3 followAccel = follow.GetSteering(target.GetComponent<Rigidbody>());
                steering.Steer(followAccel);
            } else {
                steering.Stop();
            }
            combat.Attack(target);
        }
    }
}
