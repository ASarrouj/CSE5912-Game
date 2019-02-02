using UnityEngine;
using System.Collections;

public class ProjectileShoot : MonoBehaviour {

    public Rigidbody projectile;
    public Transform bulletSpawn;
    public float projectileForce = 500f;
    public float fireRate = .25f;

    private float nextFireTime;
    private Camera gunCamera;

    void Start()
    {
        gunCamera = GetComponentInParent<Camera>();
    }

    
    // Update is called once per frame
    void Update () 
    {
        if (Input.GetButtonDown ("Fire1") && Time.time > nextFireTime) 
        {
            Rigidbody cloneRb = Instantiate (projectile, bulletSpawn.position, Quaternion.identity) as Rigidbody;
            cloneRb.AddForce(bulletSpawn.transform.forward * projectileForce);
            nextFireTime = Time.time + fireRate;
        }
    }
}