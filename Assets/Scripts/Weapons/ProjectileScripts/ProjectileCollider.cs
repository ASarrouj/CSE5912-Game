using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ProjectileCollider : NetworkBehaviour
{
    public GameObject explosion;
    public AudioClip clip;
    public int damage;
    
    public void Force(Vector3 direct)
    {
        if (!isServer)
        {
            RpcForce(direct);
        }
        else
        {
            CmdForce(direct);
        }
    }

    [ClientRpc]
    public void RpcForce(Vector3 direct)
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        gameObject.GetComponent<Rigidbody>().AddForce(direct, ForceMode.Impulse);
        Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
    }

    [Command]
    public void CmdForce(Vector3 direct)
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        gameObject.GetComponent<Rigidbody>().AddForce(direct, ForceMode.Impulse);
        Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
    }

    void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, transform.position, transform.rotation);
        GameObject sound = GameObject.Find("SoundManager");
        AudioSource source = sound.GetComponent<AudioSource>();
        source.PlayOneShot(clip, 1f);
        this.gameObject.SetActive(false);
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Player"))
        {
            DamageOverNetwork dmgHandler = collision.gameObject.GetComponent<DamageOverNetwork>();
            NetworkIdentity id = collision.gameObject.GetComponent<NetworkIdentity>();
            string hitBoxName = collision.GetContact(0).otherCollider.gameObject.name;
            Debug.Log(hitBoxName);
            dmgHandler.DamagePlayer(damage, hitBoxName, id);
        }

        if (collision.gameObject.CompareTag("Building"))
        {
            collision.gameObject.GetComponent<IDamagable>().Damage(damage);
            SwapToBroken toSwitch = collision.gameObject.GetComponentInParent<SwapToBroken>();
            if (toSwitch != null)
            {
                toSwitch.swapToBroken();
            }
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, 8f);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(1000, explosionPos, 1.8f);
            }
        }
    }
}