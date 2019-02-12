using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInput : MonoBehaviour
{
    public GameObject currentGun;

    private IWeapon gunBehavior;

    private void Awake()
    {
        gunBehavior = currentGun.GetComponent<IWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            gunBehavior.Shoot();
        }
    }
}
