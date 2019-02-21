using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class HideAgent : MonoBehaviour
    {
        
        public GameObject target;
        public List<GameObject> obstacleParents;

        private List<Rigidbody> obstacles;

        private Vector3 hidePosition;

        Rigidbody rb;

        Steering steering;
        Hide hide;


        private void Awake() {
            steering = GetComponent<Steering>();
            hide = GetComponent<Hide>();

            
        }

        // Start is called before the first frame update
        void Start()
        {
            obstacles = new List<Rigidbody>();
            foreach (GameObject o in obstacleParents) {
                GetObstacles(o);
            }
            rb = GetComponent<Rigidbody>();

            
            

        }

        void GetObstacles(GameObject o) {
            int numChildren = o.transform.childCount;
            if (numChildren > 0) {
                for (int i = 0; i < numChildren; i++) {
                    GameObject c = o.transform.GetChild(i).gameObject;
                    GetObstacles(c);
                }
            } else {
                obstacles.Add(o.GetComponent<Rigidbody>());
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (target == null) {
                target = GameObject.FindGameObjectWithTag("Player");
            } else {

            Vector3 hideAccel = hide.GetSteering(target.GetComponent<Rigidbody>(), obstacles);
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
