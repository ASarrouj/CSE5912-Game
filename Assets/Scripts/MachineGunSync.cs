﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MachineGunSync : NetworkBehaviour
{
    private ObjectPooler roundPool;
    // Start is called before the first frame update
    void Start()
    {
        roundPool = GameObject.Find("ArtProjectilePool").GetComponent<ObjectPooler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (isServer)
        {
            RpcDrawMuzzleFlash(gameObject);
        }
        else
        {
            CmdDrawMuzzleFlash(gameObject);
        }
    }

    public void Hit(Vector3 hit_point)
    {
        if (isServer)
        {
            RpcDrawHit(hit_point);
        }
        else
        {
            CmdDrawHit(hit_point);
        }
    }

    [Command]
    void CmdDrawMuzzleFlash(GameObject mech)
    {
        RpcDrawMuzzleFlash(mech);
    }

    [ClientRpc]
    void RpcDrawMuzzleFlash(GameObject mech)
    {
        if (!isLocalPlayer)
        {
            StartCoroutine(mech.GetComponentInChildren<RayCastShoot>().ShotEffect());
        }
    }

    [Command]
    void CmdDrawHit(Vector3 hit_point)
    {
        RpcDrawHit(hit_point);
    }

    [ClientRpc]
    void RpcDrawHit(Vector3 hit_point)
    {
        if (!isLocalPlayer)
        {
            gameObject.GetComponentInChildren<RayCastShoot>().ShotHit(hit_point);
        }
    }

    [Command]
    public void CmdSpawnProjectile(Vector3 position, Quaternion rotation, Vector3 forward, float projectileForce)
    {
        GameObject newProjectile = roundPool.GetObject();

        newProjectile.transform.position = position + forward * .4f;
        newProjectile.transform.rotation = rotation;
        newProjectile.transform.Rotate(new Vector3(90, 0, 0));
        newProjectile.SetActive(true);
        newProjectile.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        newProjectile.GetComponent<Rigidbody>().AddForce(forward * projectileForce, ForceMode.Impulse);

        NetworkServer.Spawn(newProjectile);
    }

    [Command]
    public void CmdArtilleryExplosion(Vector3 position)
    {
        ;
    }
}
