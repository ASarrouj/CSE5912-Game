using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour
{
    public GameObject explosion;
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
            other.gameObject.GetComponent<Renderer>().material.color = Color.red;
            Instantiate(explosion,transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}