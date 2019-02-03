using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShoot : MonoBehaviour , IWeapon
{
    public float fireRate = 0.1f;
    public float range = 50;
    public Transform gunEnd;
    public ParticleSystem gunSmoke;
    public GameObject hitEffect;
    public GameObject muzzleFlash;

    private Camera gunCamera;
    private LineRenderer lineRenderer;
    private WaitForSeconds shotLength = new WaitForSeconds(0.05f);
    private float nextFireTime;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gunCamera = GetComponentInParent<Camera>();
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
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                lineRenderer.SetPosition(0, gunEnd.position);
                lineRenderer.SetPosition(1, hit.point);
            }

            StartCoroutine(ShotEffect());
        }


    }

    private IEnumerator ShotEffect()
    {
        lineRenderer.enabled = true;
        muzzleFlash.SetActive(true);
        yield return shotLength;
        lineRenderer.enabled = false;
        muzzleFlash.SetActive(false);
    }
}
