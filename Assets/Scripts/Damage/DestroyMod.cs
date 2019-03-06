using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DestroyMod : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode(GameObject mod, GameObject particleEffect, MechTakeDamage.Hitbox hitboxType) {
        if (hitboxType == MechTakeDamage.Hitbox.CoreHitbox) {
            RpcExplodingCore(gameObject, particleEffect);
        } else if (hitboxType == MechTakeDamage.Hitbox.FrontHitbox) {
            RpcExplodingFront(gameObject, particleEffect);
        } else {
            RpcExploding(gameObject, particleEffect);
        }
    }

    [ClientRpc]
    public void RpcExploding(GameObject mod, GameObject particleEffect) {
        Debug.Log(particleEffect);
        GameObject explosion = Instantiate(particleEffect, mod.transform.position, Quaternion.identity);
        //explosion.AddComponent<NetworkIdentity>();
        //NetworkServer.Spawn(explosion);
        Destroy(mod.transform.parent.gameObject);
        Destroy(explosion, 3f);
    }

    [ClientRpc]
    public void RpcExplodingCore(GameObject mod, GameObject particleEffect) {
        GameObject explosion = Instantiate(particleEffect, mod.transform.position, Quaternion.identity);
        explosion.transform.localScale += new Vector3(1f, 1f, 1f);
        //explosion.AddComponent<NetworkIdentity>();
        //NetworkServer.Spawn(explosion);
        Destroy(explosion, 3f);
        Destroy(mod.transform.parent.gameObject);
    }
    [ClientRpc]
    public void RpcExplodingFront(GameObject mod, GameObject particleEffect) {
        GameObject explosion = Instantiate(particleEffect, mod.transform.position, Quaternion.identity);
        explosion.transform.localScale -= new Vector3(1f, 1f, 1f);
        //explosion.AddComponent<NetworkIdentity>();
        //NetworkServer.Spawn(explosion);
        Destroy(explosion, 3f);
        Destroy(mod.transform.parent.gameObject);
    }
}
