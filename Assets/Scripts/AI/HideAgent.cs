using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class HideAgent : MonoBehaviour
    {
        
        public GameObject target;

        private List<Rigidbody> obstacleRBs;
        private Vector3 hidePosition;
        private Rigidbody rb;
        private Steering steering;
        private Hide hide;


        private void Awake() {
            steering = GetComponent<Steering>();
            hide = GetComponent<Hide>();  
        }

        // Start is called before the first frame update
        void Start()
        {
            obstacleRBs = GetObstacles();
            rb = GetComponent<Rigidbody>();
        }

        private List<Rigidbody> GetObstacles() {
            List<Rigidbody> obstRB = new List<Rigidbody>();
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Building");
            foreach (GameObject g in obstacles) {
                obstRB.Add(g.GetComponent<Rigidbody>());
            }
            return obstRB;
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null) {
                target = GameObject.FindGameObjectWithTag("Player");
            } else {

            Vector3 hideAccel = hide.GetSteering(target.GetComponent<Rigidbody>(), obstacleRBs);
            steering.Steer(hideAccel);
            
            //Vector3 v = rb.velocity;
            //rb.velocity = new Vector3(v.x, 0, v.z);

            //Vector3 r = rb.rotation.eulerAngles;
           // rb.rotation = Quaternion.Euler(new Vector3(0, r.y, 0));

            //steering.LookAtDirection(rb.velocity);

                //Debug.Log(rb.velocity);

            }
        }
    }
}
