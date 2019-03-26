using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RayCastShoot : MonoBehaviour, IWeapon
{
    public float fireRate = 0.1f;
    public float range = 50;
    public Transform gunEnd;
    public ParticleSystem gunSmoke;
    public GameObject muzzleFlash;
    public Text scoreText;

    private Camera gunCamera;
    private ObjectPooler hitEffectPool;
    private LineRenderer lineRenderer;
    private WaitForSeconds shotLength = new WaitForSeconds(0.05f);
    private Score score;
    private float nextFireTime;
    private bool ifScore;
    private int scoreNum = 50;
    private DamageOverNetwork dmgOverNet;

    void Start() {
        dmgOverNet = transform.root.GetComponent<DamageOverNetwork>();
    }

    void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gunCamera = transform.Find("PlayerCamera").GetComponent<Camera>();
        hitEffectPool = GameObject.Find("MGImpactPool").GetComponent<ObjectPooler>();
        score = scoreText.GetComponent<Score>();
    }

    void IWeapon.Shoot()
    {
        GameObject mech = transform.parent.parent.parent.gameObject;
        MachineGunSync gunScript = mech.GetComponent<MachineGunSync>();

        RaycastHit hit;
        //Vector3 rayOrigin = gunCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Vector3 rayOrigin = gunEnd.position;
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            if (Physics.Raycast(rayOrigin, gunCamera.transform.forward, out hit, range))
            {
                if (hit.collider.gameObject.tag == "Target")
                {
                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * 100f);
                    }
                    hit.collider.gameObject.GetComponent<TargetTakeDamage>().Damage(10);
                }
                if (hit.collider.gameObject.tag == "Building")
                {
                    hit.collider.gameObject.GetComponent<TargetTakeDamage>().Damage(10);
                }
                if (hit.collider.gameObject.tag == "Mech")
                {
                    if (hit.rigidbody != null)
                    {
                        Transform player = hit.collider.gameObject.transform.root;
                        hit.rigidbody.AddForce(-hit.normal * 100f);
                        dmgOverNet.DamagePlayer(10, hit.collider.gameObject.name, hit.collider.transform.root.GetComponent<NetworkIdentity>());
                        if (player.GetComponent<PlayerHealth>().coreDestroyed) {
                            score.scoreUp(scoreNum);
                        }
                    }
                }
                lineRenderer.SetPosition(0, gunEnd.position);
                lineRenderer.SetPosition(1, hit.point);

                ShotHit(new Vector3(hit.point.x, hit.point.y, hit.point.z));
                gunScript.Hit(new Vector3(hit.point.x, hit.point.y, hit.point.z));
            }

            StartCoroutine(ShotEffect());
            
            gunScript.Shoot();
            
        }
    }

    public void ToggleActive()
    {
        this.enabled = !this.enabled;
    }

    public IEnumerator ShotEffect()
    {
        //lineRenderer.enabled = true;
        muzzleFlash.SetActive(true);

        yield return shotLength;
        //lineRenderer.enabled = false;
        muzzleFlash.SetActive(false);
    }

    public void ShotHit(Vector3 point)
    {
        if (hitEffectPool == null)
        {
            hitEffectPool = GameObject.Find("MGImpactPool").GetComponent<ObjectPooler>();
        }

        GameObject hitEffect = hitEffectPool.GetObject();
        hitEffect.transform.position = point;
        hitEffect.transform.rotation = Quaternion.identity;
        hitEffect.SetActive(true);
    }
}
