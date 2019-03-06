using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour
{
    public GameObject explosion;
    public AudioClip clip;
    public int damage;
    void Start()
    {
    }

    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            collision.gameObject.GetComponent<IDamagable>().Damage(damage);
            Instantiate(explosion,transform.position, transform.rotation);
            GameObject sound=GameObject.Find("SoundManager");
            AudioSource source=sound.GetComponent<AudioSource>();
            source.PlayOneShot(clip,1f);
            this.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Mech"))
        {
            collision.gameObject.GetComponent<IDamagable>().Damage(damage);
            Instantiate(explosion,transform.position, transform.rotation);
            GameObject sound=GameObject.Find("SoundManager");
            AudioSource source=sound.GetComponent<AudioSource>();
            source.PlayOneShot(clip,1f);
            this.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Building"))
        {
            collision.gameObject.GetComponent<IDamagable>().Damage(damage);
            Instantiate(explosion,transform.position, transform.rotation);
            GameObject sound=GameObject.Find("SoundManager");
            AudioSource source=sound.GetComponent<AudioSource>();
            source.PlayOneShot(clip,1f);
            collision.gameObject.GetComponent<SwapToBroken>().swapToBroken();
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, 8f);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(1000, explosionPos, 1.5f);
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