﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MechTakeDamage : NetworkBehaviour, IDamagable
{
    public bool Invincible;
    public enum Hitbox { FrontHitbox, LeftHitbox, RightHitbox, RearHitbox, CoreHitbox }
    public Hitbox hitboxType;
    public int health = 30;
    public GameObject explosionEffect;

    private PlayerInput pInput;
    private bool exploding = false;

    private NetworkIdentity networkIdentity;

    // Start is called before the first frame update
    void Start()
    {
        pInput = GetComponentInParent<PlayerInput>();
        networkIdentity = transform.root.GetComponent<NetworkIdentity>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // instead of this call DamageOverNetwork.DamagePlayer()
    public bool Damage(int dmgAmount)
    {
        return false;
    }

    public void Exploding()
    {
        if (!exploding) {
            exploding = true;
            if (networkIdentity.isServer) Explode(transform.position);
            ForceCameraSwitch();
            DestroyNetChild();
            transform.root.GetComponent<GunAttacher>().CmdDestroyGun(transform.parent.name);
        }
    }

    private void Explode(Vector3 pos) {
        GameObject explosion = Instantiate(explosionEffect, pos, Quaternion.identity);
        NetworkServer.Spawn(explosion);
    }

    //public void ExplodingCore()
    //{
    //    coreDestroyed = true;
    //    GameObject explosion = Instantiate(particleEffects[1], transform.position, Quaternion.identity);
    //    explosion.transform.localScale += new Vector3(1f, 1f, 1f);
    //    explosion.AddComponent<NetworkIdentity>();
    //    NetworkServer.Spawn(explosion);
    //    ForceCameraSwitch();
    //    Destroy(explosion, 3f);
    //    DestroyNetChild();
    //    Destroy(transform.parent.gameObject);
    //}

    //public void ExplodingFront()
    //{
    //    GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
    //    explosion.transform.localScale -= new Vector3(1f, 1f, 1f);
    //    explosion.AddComponent<NetworkIdentity>();
    //    NetworkServer.Spawn(explosion);
    //    ForceCameraSwitch();
    //    Destroy(explosion, 3f);
    //    DestroyNetChild();
    //    Destroy(transform.parent.gameObject);
    //}

    private void DestroyNetChild()
    {
        Transform wep = null;
        foreach (Transform c in transform.parent)
        {
            if (c.tag == "Weapon")
            {
                wep = c;
                break;
            }
        }
        if (wep == null)
        {
            return;
        }
        NetworkTransformChild[] netChildren = transform.root.GetComponents<NetworkTransformChild>();
        foreach (NetworkTransformChild c in netChildren)
        {
            if (c.target == wep)
            {
                c.enabled = false;
                break;
            }
        }
    }

    private void ForceCameraSwitch()
    {
        Transform cam = null;
        foreach (Transform c in transform.parent)
        {
            cam = c.Find("PlayerCamera");
            if (cam != null) break;
        }
        if (cam != null) cam.parent = transform.root;
        if (transform.root.CompareTag("Player")) pInput.PrepareMechPerspec();
    }
}
