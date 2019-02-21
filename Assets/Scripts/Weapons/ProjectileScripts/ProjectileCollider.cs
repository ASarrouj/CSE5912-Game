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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Target"))
        {
            other.gameObject.GetComponent<TargetTakeDamage>().Damage(damage);
            Instantiate(explosion,transform.position, transform.rotation);
            GameObject sound=GameObject.Find("SoundManager");
            AudioSource source=sound.GetComponent<AudioSource>();
            source.PlayOneShot(clip,1f);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Mech"))
        {
            other.gameObject.GetComponent<TargetTakeDamage>().Damage(damage);
            Instantiate(explosion,transform.position, transform.rotation);
            GameObject sound=GameObject.Find("SoundManager");
            AudioSource source=sound.GetComponent<AudioSource>();
            source.PlayOneShot(clip,1f);
            Destroy(gameObject);
        }
    }
}