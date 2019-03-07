using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MachineGunSync : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
}
