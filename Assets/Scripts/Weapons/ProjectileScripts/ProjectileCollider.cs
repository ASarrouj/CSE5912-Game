using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ProjectileCollider : NetworkBehaviour
{
    public GameObject explosion;
    public AudioClip clip;
    public int damage;

    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {

    }
    public void Force(Vector3 direct)
    {
    if(!isServer)
    {
    RpcForce(direct);
    }
    else{
    CmdForce(direct);
    }
    }
    [ClientRpc]
    public void RpcForce(Vector3 direct)
    {
        gameObject.GetComponent<Rigidbody>().velocity=new Vector3(0f,0f,0f);
        gameObject.GetComponent<Rigidbody>().AddForce(direct,ForceMode.Impulse);
        Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
    }
    [Command]
    public void CmdForce(Vector3 direct)
    {
        gameObject.GetComponent<Rigidbody>().velocity=new Vector3(0f,0f,0f);
        gameObject.GetComponent<Rigidbody>().AddForce(direct,ForceMode.Impulse);
        Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            collision.gameObject.GetComponent<IDamagable>().Damage(damage);
            Instantiate(explosion,transform.position, transform.rotation);
            source.PlayOneShot(clip,1f);
            this.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Mech"))
        {
            Transform mech = collision.transform.root;
            mech.GetComponent<DamageOverNetwork>().DamagePlayer(damage, collision.gameObject.name, mech.GetComponent<NetworkIdentity>());
            Instantiate(explosion,transform.position, transform.rotation);
            GameObject sound=GameObject.Find("SoundManager");
            AudioSource source=sound.GetComponent<AudioSource>();
            source.PlayOneShot(clip,1f);
            this.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Building"))
        {
            Debug.Log("Collide Building");
            collision.gameObject.GetComponent<IDamagable>().Damage(damage);
            Instantiate(explosion,transform.position, transform.rotation);
            GameObject sound=GameObject.Find("SoundManager");
            AudioSource source=sound.GetComponent<AudioSource>();
            source.PlayOneShot(clip,1f);
            SwapToBroken toSwitch = collision.gameObject.GetComponentInParent<SwapToBroken>();
            if(toSwitch != null)
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
            this.gameObject.SetActive(false);
        }
        if (collision.gameObject.layer==8)
        {
            Instantiate(explosion,transform.position, transform.rotation);
            GameObject sound=GameObject.Find("SoundManager");
            AudioSource source=sound.GetComponent<AudioSource>();
            source.PlayOneShot(clip,1f);
            this.gameObject.SetActive(false);
        }
    }
}