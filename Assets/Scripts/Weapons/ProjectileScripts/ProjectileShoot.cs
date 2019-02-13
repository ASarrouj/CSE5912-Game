using UnityEngine;
using System.Collections;
using UnityEditor;

public class ProjectileShoot : MonoBehaviour, IWeapon
{
    public Transform bulletSpawn;
    public ParticleSystem smoke;
    public ObjectPooler roundPool;
    public float projectileForce = 5000f;
    public float fireRate = 1f;

    private LineRenderer line;
    private float nextFireTime;
    private Camera gunCamera;
    private AudioSource source;
    private WaitForSeconds shotLength = new WaitForSeconds(0.1f);

    void Awake()
    {
        gunCamera = transform.parent.Find("PlayerCamera").GetComponent<Camera>();
        source = GetComponent<AudioSource>();
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        Plot(bulletSpawn.transform.position, projectileForce * bulletSpawn.transform.forward, .1f, 3f);
    }

    public void Plot(Vector3 start, Vector3 startVelocity, float time, float maxTime)
    {
        Vector3[] positions = new Vector3[Mathf.RoundToInt(maxTime / time) + 1];
        line.positionCount = positions.Length;
        positions[0] = start;
        for (int i = 1; ; i++)
        {
            float t = time * i;
            if (t > maxTime) break;
            Vector3 pos = PlotTrajectoryAtTime(start, startVelocity, t);
            positions[i] = pos;
        }
        line.SetPositions(positions);
    }

    public Vector3 PlotTrajectoryAtTime(Vector3 start, Vector3 startVelocity, float time)
    {
        return start + startVelocity * time + Physics.gravity * time * time * 0.5f;
    }

    void IWeapon.Shoot()
    {
        nextFireTime = Time.time + fireRate;

        GameObject projectile = roundPool.GetObject();
        projectile.transform.position = bulletSpawn.position;
        projectile.transform.rotation = Quaternion.identity;
        projectile.GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.forward * projectileForce, ForceMode.Impulse);
        StartCoroutine(ShotEffect());
    }

    private IEnumerator ShotEffect()
    {
        smoke.Play();
        source.Play();
        yield return shotLength;

    }
}