using UnityEngine;
using System.Collections;

public class ProjectileShoot : MonoBehaviour, IWeapon {

    public Rigidbody projectile;
    public Transform bulletSpawn;
    public GameObject smoke;
    public float projectileForce = 500f;
    public float fireRate = 1f;

    private float nextFireTime;
    private Camera gunCamera;

    void Awake()
    {
        gunCamera = GetComponentInParent<Camera>();
    }

    
    // Update is called once per frame
    void IWeapon.Shoot()
    {
        if (Input.GetButtonDown ("Fire1") && Time.time > nextFireTime) 
        {
            Rigidbody cloneRb = Instantiate (projectile, bulletSpawn.position, Quaternion.identity) as Rigidbody;
            cloneRb.AddForce(bulletSpawn.transform.forward * projectileForce);
            Instantiate(smoke,bulletSpawn.position,bulletSpawn.rotation);
            nextFireTime = Time.time + fireRate;
        }
    }
}