using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class Combat : MonoBehaviour
    {
        private Transform mech;
        public List<Transform> weapons;

        private Transform currentWep;
        private IWeapon input;

        private Transform cam;

        void Awake()
        {
            cam = transform.Find("PlayerCamera");
            weapons = new List<Transform>();
            foreach (Transform t in transform) {
                if (t.tag == "Mech") {
                    mech = t;
                    break;
                }
            }
            currentWep = mech.Find("FrontGun").GetChild(0);
            weapons.Add(currentWep);
            weapons.Add(mech.Find("LeftGun").GetChild(0));
            input = currentWep.GetComponent<IWeapon>();

            cam.parent = currentWep;
            cam.localPosition = new Vector3(0, 2.5f, 0);
            cam.localRotation = Quaternion.identity;

            input.ToggleActive();
            currentWep.GetComponent<LineRenderer>().enabled = true;
            
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void Attack(GameObject target) {

            //Vector3 targetVelocity = target.transform.position - transform.position;

            //float dist = targetVelocity.magnitude;
            //input.ToggleActive();

            currentWep.LookAt(target.transform);
            Vector3 rot = currentWep.localRotation.eulerAngles;

            if (!(rot.y > 40 && rot.y < 320)) {
                input.Shoot();
            }

            if (rot.y > 30 && rot.y < 180) {
                rot.y = 30;
                currentWep.localRotation = Quaternion.Euler(rot);
            } else if (rot.y < 330 && rot.y > 180) {
                rot.y = 330;
                currentWep.localRotation = Quaternion.Euler(rot);
            }

            

            //input.ToggleActive();

        }
    }
}
