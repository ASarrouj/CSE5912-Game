using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkObjectDisable : MonoBehaviour
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
