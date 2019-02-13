using UnityEngine;
using System.Collections;
using UnityEditor;

public class ProjectileShoot : MonoBehaviour, IWeapon{

    public Rigidbody projectile;
    public Transform bulletSpawn;
    public GameObject smoke;
    public LineRenderer line;
    public float projectileForce = 5000f;
    public float fireRate = 1f;
    bool cameraOn;
    bool disabled;

    private float nextFireTime;
    private Camera gunCamera;

    void Awake()
    {
        gunCamera = GetComponentInParent<Camera>();
        disabled=false;
    }

    void Update()
    {
        Plot(bulletSpawn.transform.position,projectileForce*bulletSpawn.transform.forward,.1f,3f);
        
    }
     public void Plot (Vector3 start, Vector3 startVelocity, float time, float maxTime) {
         Vector3[] positions=new Vector3[Mathf.RoundToInt(maxTime/time)+1];
         line.SetVertexCount(positions.Length);
     positions[0] = start;
     for (int i=1;;i++) {
         float t = time*i;
         if (t > maxTime) break;
         Vector3 pos = PlotTrajectoryAtTime (start, startVelocity, t);
         positions[i]=pos;
     }
     line.SetPositions(positions);
 }
  public Vector3 PlotTrajectoryAtTime (Vector3 start, Vector3 startVelocity, float time) {
     return start + startVelocity*time + Physics.gravity*time*time*0.5f;
 }

    void IWeapon.Shoot()
    {
        if (Input.GetButtonDown ("Fire1") && Time.time > nextFireTime) 
        {
            Rigidbody cloneRb = Instantiate (projectile, bulletSpawn.position, Quaternion.identity) as Rigidbody;
            cloneRb.AddForce(bulletSpawn.transform.forward * projectileForce,ForceMode.Impulse);
            Instantiate(smoke,bulletSpawn.position,bulletSpawn.rotation);
            nextFireTime = Time.time + fireRate;
        }
    }
}