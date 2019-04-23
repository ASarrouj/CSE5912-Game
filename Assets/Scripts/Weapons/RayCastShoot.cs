﻿using System;
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
    public Camera gunCamera;

    private ObjectPooler hitEffectPool;
    private LineRenderer lineRenderer;
    private WaitForSeconds shotLength = new WaitForSeconds(0.05f);
    private Score score;
    private float nextFireTime;
    private bool ifScore;
    private int scoreNum = 50;
    private DamageOverNetwork dmgOverNet;
    private MachineGunSync gunScript;

    void Start()
    {
        dmgOverNet = transform.root.GetComponent<DamageOverNetwork>();
        gunScript = transform.root.GetComponent<MachineGunSync>();
    }

    void OnEnable()
    {
        
        lineRenderer = GetComponent<LineRenderer>();
        hitEffectPool = GameObject.Find("MGImpactPool").GetComponent<ObjectPooler>();
        score = transform.root.GetComponent<Score>();
    }

    public void GetCameraAndScore(Camera cam)
    {
        gunCamera = cam;
        //score = toGetScore.GetComponent<Score>();
    }

    void IWeapon.Shoot()
    {
        gunScript = transform.root.GetComponent<MachineGunSync>();
        RaycastHit hit;
        //Vector3 rayOrigin = gunCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Vector3 rayOrigin = gunEnd.position;
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            if (Physics.Raycast(rayOrigin, gunCamera.transform.forward, out hit, range))
            {
                NetworkIdentity targetID = null;
                if (hit.rigidbody != null)
                {
                    if (hit.collider.gameObject.layer == 10) //player object
                    {
                        Debug.Log(hit.rigidbody.gameObject.name);
                        dmgOverNet = transform.root.GetComponent<DamageOverNetwork>();
                        //DamageOverNetwork dmgHandler = hit.rigidbody.gameObject.GetComponent<DamageOverNetwork>();
                        targetID = hit.rigidbody.gameObject.GetComponent<NetworkIdentity>();
                        string hitBoxName = hit.collider.gameObject.name;
                        Debug.Log(hitBoxName);
                        dmgOverNet.DamagePlayer(5, hitBoxName, targetID);
                        if (hit.rigidbody.gameObject.GetComponent<PlayerHealth>().coreDestroyed)
                        {
                            score.ScoreUp(scoreNum);
                        }
                    }
                }
                if (hit.collider.gameObject.tag == "Mine")
                {
                    hit.collider.gameObject.GetComponent<MineController>().BlowUpMine();
                }
                lineRenderer.SetPosition(0, gunEnd.position);
                lineRenderer.SetPosition(1, hit.point);

                ShotHit(new Vector3(hit.point.x, hit.point.y, hit.point.z), targetID);
                gunScript.Hit(new Vector3(hit.point.x, hit.point.y, hit.point.z), targetID);
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

    public void ShotHit(Vector3 point, NetworkIdentity targetID)
    {
        if (hitEffectPool == null)
        {
            hitEffectPool = GameObject.Find("MGImpactPool").GetComponent<ObjectPooler>();
        }

        GameObject newBullet = hitEffectPool.GetObject();
        newBullet.transform.position = point;
        newBullet.transform.rotation = Quaternion.identity;
        newBullet.SetActive(true);

        NetworkServer.Spawn(newBullet);
    }
}
