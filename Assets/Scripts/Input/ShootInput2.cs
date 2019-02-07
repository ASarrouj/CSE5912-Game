using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInputArtillery : MonoBehaviour
{
    public GameObject currentGun;

    private IWeapon gunBehavior;
    public AudioClip aud;
    AudioSource source;

    private void Awake()
    {
        gunBehavior = currentGun.GetComponent<IWeapon>();
        source=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetButton("Fire1"))
        {
            gunBehavior.Shoot();
            source.PlayOneShot(aud,.3f);
        }
    }
}
