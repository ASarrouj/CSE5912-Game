using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWeapon : MonoBehaviour
{
    public float fireRate = 0.25f;
    public float range = 50;
    public Transform gunEnd;

    private Camera gunCamera;
    private LineRenderer lineRenderer;
    private float nextFireTime;

}
