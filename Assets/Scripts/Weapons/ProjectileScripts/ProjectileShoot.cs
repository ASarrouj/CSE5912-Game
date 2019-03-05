using UnityEngine;
using System.Collections;
using UnityEditor;

public class ProjectileShoot : MonoBehaviour, IWeapon
{
    public Transform bulletSpawn;
    public GameObject smoke;
    public float projectileForce = 5000f;
    public float fireRate = 1f;

    private LineRenderer line;
    private float nextFireTime;
    private ObjectPooler roundPool;
    private AudioSource source;
    private WaitForSeconds shotLength = new WaitForSeconds(0.7f);

    void OnEnable()
    {
        source = GetComponent<AudioSource>();
        line = transform.Find("WeaponUI").GetComponent<LineRenderer>();
        roundPool = GameObject.Find("ArtProjectilePool").GetComponent<ObjectPooler>();
    }

    void Update()
    {
        if (line.enabled)
        {
            Plot(gameObject.transform.position, projectileForce * bulletSpawn.forward, .1f, 3f);
        }
    }

    public void ToggleActive()
    {
        this.enabled = !this.enabled;
    }

    public void Plot(Vector3 start, Vector3 startVelocity, float time, float maxTime)
    {
        Vector3[] positions = new Vector3[Mathf.RoundToInt(maxTime / time) + 1];
        line.positionCount = positions.Length;
        positions[0] = start;

        // bad hack: set all points up to 0.2*number of points = point at t=0.2*total_time
        int half = (int)((Mathf.RoundToInt(maxTime / time) + 1) * 0.2f);
        float halftime = time * half;

        for (int i = 0; i < half; i++)
        {
            if (halftime > maxTime) break;
            Vector3 pos = PlotTrajectoryAtTime(start, startVelocity, halftime);
            positions[i] = pos;
        }

        for (int i = half; ; i++)
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
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            GameObject projectile = roundPool.GetObject();
            projectile.transform.position = bulletSpawn.position+bulletSpawn.forward*.4f;
            projectile.transform.rotation = bulletSpawn.rotation;
            projectile.transform.Rotate(new Vector3(90, 0, 0));
            projectile.SetActive(true);
            projectile.GetComponent<Rigidbody>().velocity=new Vector3(0f,0f,0f);
            projectile.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * projectileForce, ForceMode.Impulse);
            StartCoroutine(ShotEffect());
        }
    }

    private IEnumerator ShotEffect()
    {
        smoke.SetActive(true);
        source.Play();
        yield return shotLength;
        smoke.SetActive(false);

    }
}