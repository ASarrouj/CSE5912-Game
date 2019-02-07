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

    void Update()
    {
        Plot(bulletSpawn.transform.forward,projectileForce*transform.forward,.05f,10f);
    }
     public void Plot (Vector3 start, Vector3 startVelocity, float time, float maxTime) {
     Vector3 prev = start;
     for (int i=1;;i++) {
         float t = time*i;
         if (t > maxTime) break;
         Vector3 pos = PlotTrajectoryAtTime (start, startVelocity, t);
         if (Physics.Linecast (prev,pos)) break;
         Debug.DrawLine (prev,pos,Color.red);
         prev = pos;
     }
 }
  public Vector3 PlotTrajectoryAtTime (Vector3 start, Vector3 startVelocity, float time) {
     return start + startVelocity*time + Physics.gravity*time*time*0.5f;
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