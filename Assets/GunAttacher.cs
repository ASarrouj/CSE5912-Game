using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GunAttacher : NetworkBehaviour
{
    public GameObject artillery;

    [Command]
    public void CmdAttachGun(GameObject gun, string parentName)
    {
        GameObject g = Instantiate(artillery);
        NetworkServer.Spawn(g);
        RpcAttachGun(g, parentName);
    }

    [ClientRpc]
    public void RpcAttachGun(GameObject gun, string parentName)
    {
        gun.transform.parent = gameObject.transform.GetChild(0).transform.Find(parentName);
        gun.transform.position = gun.transform.parent.position;
        gun.transform.rotation = gun.transform.parent.rotation;
    }
}