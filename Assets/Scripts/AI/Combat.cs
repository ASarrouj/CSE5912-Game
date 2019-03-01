using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class Combat : MonoBehaviour
    {
        private Transform mech;
        public List<Transform> weapons;

        // Start is called before the first frame update
        void Start()
        {
            weapons = new List<Transform>();
            foreach (GameObject g in transform) {
                if (g.tag == "Mech") {
                    mech = g.transform;
                    break;
                }
            }
            weapons.Add(mech.Find("FrontGun").GetChild(0));
            weapons.Add(mech.Find("LeftGun").GetChild(0));
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Attack() {

        }
    }
}
