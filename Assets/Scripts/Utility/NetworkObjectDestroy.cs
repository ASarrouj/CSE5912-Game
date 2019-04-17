using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkObjectDestroy : NetworkBehaviour
{
    public float existTime = 2f;

    private void OnEnable()
    {
        Invoke("Destroy", existTime);
    }

    [Command]
    void CmdDestroy()
    {
        Destroy();
        RpcDestroy();
    }

    [ClientRpc]
    void RpcDestroy()
    {
        Destroy();
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
