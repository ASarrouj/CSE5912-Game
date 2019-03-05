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

    void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gunCamera = transform.Find("PlayerCamera").GetComponent<Camera>();
        hitEffectPool = GameObject.Find("MGImpactPool").GetComponent<ObjectPooler>();
        score = scoreText.GetComponent<Score>();
    }

    void IWeapon.Shoot()
    {
        RaycastHit hit;
        Vector3 rayOrigin = gunCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
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
                        hit.rigidbody.AddForce(-hit.normal * 100f);
                    }
                    ifScore = hit.collider.gameObject.GetComponent<MechTakeDamage>().Damage(10);
                    if (ifScore)
                    {
                        score.scoreUp(scoreNum);
                    }
                }
                lineRenderer.SetPosition(0, gunEnd.position);
                lineRenderer.SetPosition(1, hit.point);
                GameObject hitEffect = hitEffectPool.GetObject();
                hitEffect.transform.position = hit.point;
                hitEffect.transform.rotation = Quaternion.identity;
                hitEffect.SetActive(true);
            }

            StartCoroutine(ShotEffect());
            
            GameObject mech = transform.parent.parent.parent.gameObject;
            MachineGunSync gunScript = mech.GetComponent<MachineGunSync>();
            gunScript.Shoot();
        }
    }

    public void ToggleActive()
    {
        this.enabled = !this.enabled;
    }

    public IEnumerator ShotEffect()
    {
        lineRenderer.enabled = true;
        muzzleFlash.SetActive(true);

        yield return shotLength;
        lineRenderer.enabled = false;
        muzzleFlash.SetActive(false);
    }
}
