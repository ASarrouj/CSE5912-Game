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
        gun.transform.position = Vector3.zero;
        gun.transform.SetParent(gameObject.transform.GetChild(0).transform.Find(parentName), false);

        if (isLocalPlayer)
        {
            gameObject.transform.Find("MechPicking").transform.GetComponent<MechBuilder>().finishAttachGunInOrder();
        }
    }

    [Command]
    public void CmdDestroyGun(string gunSlot) {
        Debug.Log(gunSlot);
        RpcDestroyGun(gunSlot);
    }

    [ClientRpc]
    public void RpcDestroyGun(string gunSlot) {
        Debug.Log(gunSlot);
        Transform mech = gameObject.transform.GetChild(0);
        if (gunSlot.Equals(mech.name)) Destroy(mech.gameObject);
        else Destroy(mech.Find(gunSlot).gameObject);
    }
}
