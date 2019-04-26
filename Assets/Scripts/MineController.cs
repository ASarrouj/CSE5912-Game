using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MineController : NetworkBehaviour
{

    private bool armed;
    public GameObject explosion;
    private GameObject light;
    private AudioSource source;

    private int scoreIndex;

    void Start()
    {
        armed = false;
        Invoke("ArmMine", 2);
        source = GetComponent<AudioSource>();
        //light = transform.parent.Find("Area Light").gameObject;
        if (transform.root.GetComponent<PlayerHealth>().isPlayer) {
            scoreIndex = transform.root.GetComponent<Prototype.NetworkLobby.PlayerStatus>().index;
        } else {
            scoreIndex = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.gameObject.tag == "Player" && armed == true)
        {
            BlowUpMine();

            DamageOverNetwork dmgHandler = other.attachedRigidbody.gameObject.GetComponent<DamageOverNetwork>();
            NetworkIdentity id = other.attachedRigidbody.gameObject.GetComponent<NetworkIdentity>();
            string hitboxName = other.gameObject.name;
            dmgHandler.DamagePlayer(30, hitboxName, id, scoreIndex);
        }
    }

    private void ArmMine()
    {
        armed = true;
        //light.SetActive(true);
    }

    private void DeleteMine()
    {
        Destroy(transform.parent.gameObject);
    }

    public void BlowUpMine()
    {
        Instantiate(explosion, transform.position, transform.rotation, transform.parent);
        source.Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, 8f);
        foreach (Collider hit in colliders)
        {
            
            Rigidbody rb = hit.attachedRigidbody;

            if (rb != null)
            {
                rb.transform.Translate(new Vector3(0, .5f, 0));
                rb.AddExplosionForce(100000, explosionPos, 3.0f);
            }
        }
        Invoke("DeleteMine", 1);
    }
}
