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
}
