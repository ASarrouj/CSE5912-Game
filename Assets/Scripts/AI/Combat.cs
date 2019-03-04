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

        private RayCastShoot rayCastShoot;
        private ProjectileShoot projectileShoot;

        private enum WeaponType
        {
            raycast,
            projectile,
            noncombat
        }

        WeaponType wepType;

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
            currentWep = mech.Find("LeftGun").GetChild(0);          
            weapons.Add(mech.Find("FrontGun").GetChild(0));
            weapons.Add(currentWep);
            input = currentWep.GetComponent<IWeapon>();

            cam.parent = currentWep;
            cam.localPosition = new Vector3(0, 2.5f, 0);
            cam.localRotation = Quaternion.identity;

            input.ToggleActive();
            currentWep.GetComponent<LineRenderer>().enabled = true;

            GetCurrentWeaponType();

        }

        // Update is called once per frame
        void Update()
        {
        }

        public void Attack(GameObject target) {

            //Vector3 targetVelocity = target.transform.position - transform.position;

            //float dist = targetVelocity.magnitude;
            //input.ToggleActive();

            switch (wepType) {
                case WeaponType.raycast:
                    RaycastShoot(target);
                    break;
                case WeaponType.projectile:
                    ProjectileShoot(target);
                    break;
                default:
                    break;
            }

        }

        private void GetCurrentWeaponType() {
            rayCastShoot = currentWep.GetComponent<RayCastShoot>();
            projectileShoot = currentWep.GetComponent<ProjectileShoot>();

            if (rayCastShoot != null) {
                wepType = WeaponType.raycast;
            } else if (projectileShoot != null) {
                wepType = WeaponType.projectile;
            } else {
                wepType = WeaponType.noncombat;
            }
        }

        private void ChangeCurrentWeapon(int index) {
            currentWep = weapons[index];
            GetCurrentWeaponType();
        }

        private void RaycastShoot(GameObject target) {

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
        }

        private void ProjectileShoot(GameObject target) {
            Vector3 projectileVelocity;
            bool valid = PredictiveAim.PredictAim(currentWep.position, projectileShoot.projectileForce, target.transform.position,
                target.GetComponent<Rigidbody>().velocity, Physics.gravity.y, out projectileVelocity);
            if (valid) {
                currentWep.rotation = Quaternion.LookRotation(projectileVelocity);
                input.Shoot();
            }
        }
    }
}

