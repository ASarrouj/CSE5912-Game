using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI {
    public class Combat : MonoBehaviour
    {
        private Transform mech;
        public Transform[] weapons;

        private Transform currentWep;
        private IWeapon input;

        private Transform cam;

        private RayCastShoot rayCastShoot;
        private ProjectileShoot projectileShoot;

        private PlayerTargeting targeting;

        private float leftFOV = 45, frontFOV = 30, rightFOV = 30, fovRad;

        private enum WeaponType
        {
            raycast,
            projectile,
            noncombat
        }

        private WeaponType wepType;
        private int wepIndex = -1;
        private int updateCount = 0, wepCheckTime = 10;

        void Awake()
        {
            targeting = GetComponent<PlayerTargeting>();

            cam = transform.Find("PlayerCamera");
            weapons = new Transform[3];
            foreach (Transform t in transform) {
                if (t.tag == "Mech") {
                    mech = t;
                    break;
                }
            }

            weapons[1] = (mech.Find("FrontGun").GetChild(0));
            weapons[2] = (mech.Find("LeftGun").GetChild(0));
            //weapons.Add(mech.Find("RightGun").GetChild(0));


            SelectWeapon(1);

            //input.ToggleActive();
            //currentWep.GetComponent<LineRenderer>().enabled = true;

            GetCurrentWeaponType();

        }

        // Update is called once per frame
        void Update()
        {
            updateCount++;
            if (updateCount > 100) {

                WeaponCheck();
                updateCount = 0;
            }
        }

        public void Attack(GameObject target) {

            //Vector3 targetVelocity = target.transform.position - transform.position;

            //float dist = targetVelocity.magnitude;
            //input.ToggleActive();

            if (currentWep == null) return;

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

        private void SelectWeapon(int index) {
            if (wepIndex == index) return;
            currentWep = weapons[index];
            wepIndex = index;
            SetRadiusFOV();
            if (currentWep == null) return;

            GetCurrentWeaponType(); 
            input = currentWep.GetComponent<IWeapon>();
            if (cam == null) {
                cam = transform.root.Find("PlayerCamera");
            }
            cam.parent = currentWep;
            cam.localPosition = new Vector3(0, 2.5f, 0);
            cam.localRotation = Quaternion.identity;
            input.ToggleActive();
        }

        private void WeaponCheck() {
            float angle = targeting.TargetAngle();
            if (angle > -frontFOV - 10 && angle < frontFOV + 10) {
                SelectWeapon(1);
            } else if (angle < -45 + leftFOV) {
                SelectWeapon(2);
            }
        }

        private void RaycastShoot(GameObject target) {

            currentWep.LookAt(target.transform);
            RestrictedShoot();
            
        }

        private void ProjectileShoot(GameObject target) {
            Vector3 projectileVelocity;
            bool valid = PredictiveAim.PredictAim(currentWep.position, projectileShoot.projectileForce, target.transform.position,
                target.GetComponent<Rigidbody>().velocity, Physics.gravity.y, out projectileVelocity);
            if (valid) {
                currentWep.rotation = Quaternion.LookRotation(projectileVelocity);
                //Debug.Log("ROT: " + currentWep.rotation.eulerAngles);
                RestrictedShoot();
            }
        }

        private void SetRadiusFOV() {
            switch (wepIndex) {
                case 2:
                    fovRad = leftFOV;
                    break;
                case 3:
                    fovRad = rightFOV;
                    break;
                default:
                    fovRad = frontFOV;
                    break;
            }
        }

        private void RestrictedShoot() {

            Vector3 rot = currentWep.localRotation.eulerAngles;

            if (!(rot.y > fovRad + 10 && rot.y < 350 - fovRad)) {
                input.Shoot();
            }

            if (rot.y > fovRad && rot.y < 180) {
                rot.y = 30;
                currentWep.localRotation = Quaternion.Euler(rot);
            } else if (rot.y < 360 - fovRad && rot.y > 180) {
                rot.y = 360 - frontFOV;
                currentWep.localRotation = Quaternion.Euler(rot);
            }
        }
    }
}

