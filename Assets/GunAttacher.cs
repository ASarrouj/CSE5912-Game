using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GunAttacher : NetworkBehaviour
{
    public GameObject[] prefabs;

    [Command]
    public void CmdAttachGun(string gunName, string parentName)
    {
        GameObject gunPrefab = Array.Find(prefabs, x => x.name == gunName);
        GameObject g = Instantiate(gunPrefab);
        NetworkServer.Spawn(g);

        RpcAttachGun(g, parentName);
    }

    [ClientRpc]
    public void RpcAttachGun(GameObject gun, string parentName)
    {
        gun.transform.SetParent(gameObject.transform.GetChild(0).transform.Find(parentName));
        gun.transform.position = gun.transform.parent.position;
        gun.transform.rotation = gun.transform.parent.rotation;

        if (isLocalPlayer)
        {
            gameObject.transform.Find("MechPicking").transform.GetComponent<MechBuilder>().finishAttachGunInOrder();
        }
    }
}