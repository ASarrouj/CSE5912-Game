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
            collision.gameObject.GetComponent<TargetTakeDamage>().Damage(damage);
            Instantiate(explosion,transform.position, transform.rotation);
            GameObject sound=GameObject.Find("SoundManager");
            AudioSource source=sound.GetComponent<AudioSource>();
            source.PlayOneShot(clip,1f);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Mech"))
        {
            collision.gameObject.GetComponent<TargetTakeDamage>().Damage(damage);
            Instantiate(explosion,transform.position, transform.rotation);
            GameObject sound=GameObject.Find("SoundManager");
            AudioSource source=sound.GetComponent<AudioSource>();
            source.PlayOneShot(clip,1f);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer==8)
        {
            Instantiate(explosion,transform.position, transform.rotation);
            GameObject sound=GameObject.Find("SoundManager");
            AudioSource source=sound.GetComponent<AudioSource>();
            source.PlayOneShot(clip,1f);
            Destroy(gameObject);
        }
    }
}