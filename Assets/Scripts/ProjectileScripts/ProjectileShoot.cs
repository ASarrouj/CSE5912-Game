using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera gunCamera;
    void Start()
    {
            gunCamera = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
