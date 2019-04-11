using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GunAttacher : NetworkBehaviour
{
    [Command]
    public void CmdAttachGun(GameObject gun, string parentName)
    {
        NetworkServer.Spawn(gun);
        RpcAttachGun(gun, parentName);
    }

    [ClientRpc]
    public void RpcAttachGun(GameObject gun, string parentName)
    {
        gun.transform.parent = gameObject.transform.GetChild(0).transform.FindChild(parentName);
    }
}