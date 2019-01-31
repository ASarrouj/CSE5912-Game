using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShoot : MonoBehaviour
{
    public float fireRate = 0.25f;
    public float range = 50;
    public Transform gunEnd;

    private Camera gunCamera;
    private LineRenderer lineRenderer;
    private float nextFireTime;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gunCamera = GetComponentInParent<Camera>();
    }
    
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 rayOrigin = gunCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        if (Input.GetButtonDown("Fire1") && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            
            if (Physics.Raycast(rayOrigin, gunCamera.transform.forward, out hit, range))
            {
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
