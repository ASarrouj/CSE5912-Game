using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MechTakeDamage : NetworkBehaviour, IDamagable
{
    public bool Invincible;
    public enum Hitbox { FrontHitbox, LeftHitbox, RightHitbox, RearHitbox, CoreHitbox}
    public Hitbox hitboxType;
    private bool coreDestroyed;
    public int health=30;
    [SerializeField] GameObject [] particleEffects;
    // Start is called before the first frame update
    void Start()
    {
        coreDestroyed = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Damage(int dmgAmount) 
    {
        if (Invincible) return coreDestroyed;
        if (hitboxType == Hitbox.FrontHitbox)
        {
            Debug.Log("Front takes damage");
            transform.parent.parent.GetComponent<PlayerHealth>().dmgFront(dmgAmount,this);
            // if (health <= 0 && particleEffects != null && particleEffects.Length > 0)
            // {
            //     RpcExplodingFront();
            //     // GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
            //     // explosion.transform.localScale -= new Vector3(1f, 1f, 1f);
            //     // Destroy(explosion, 3f);
            //     // Destroy(transform.parent.gameObject);
            // }
        }

        if (hitboxType == Hitbox.LeftHitbox)
        {
            Debug.Log("Left takes damage");

                        transform.parent.parent.GetComponent<PlayerHealth>().dmgLeft(dmgAmount,this);
            // if (health <= 0 && /*particleEffects != null &&*/particleEffects.Length > 0)
            // {
            //     RpcExploding();
            //     // GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
            //     // Destroy(transform.parent.gameObject);
            //     // Destroy(explosion, 3f);
            // }
        }
        if (hitboxType == Hitbox.RightHitbox)
        {
            Debug.Log("Right takes damage");             
            transform.parent.parent.GetComponent<PlayerHealth>().dmgRight(dmgAmount,this);
            //     if (health <= 0 && /*particleEffects != null &&*/particleEffects.Length > 0)
            //     {
            //     // RpcExploding();
            //     // GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
            //     // Destroy(transform.parent.gameObject);
            //     // Destroy(explosion, 3f);
            // }


        }
        if (hitboxType == Hitbox.CoreHitbox)
        {
            Debug.Log("Core takes " + dmgAmount + " damage");
            
            coreDestroyed=transform.parent.parent.GetComponent<PlayerHealth>().dmgCore(dmgAmount,this);
            GameObject flames = Instantiate(particleEffects[1], transform.position, Quaternion.identity, transform);
            flames.transform.localScale += new Vector3(1f, 1f, 1f);
            // if (health <= 0)
            // {

                // RpcExplodingCore();
                // GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
                // explosion.transform.localScale += new Vector3(1f, 1f, 1f);
                // Destroy(explosion, 3f);
                // Destroy(transform.parent.gameObject);
            // }
        }

        if (hitboxType == Hitbox.RearHitbox)
        {
            Debug.Log("Rear takes damage");
            transform.parent.parent.GetComponent<PlayerHealth>().dmgRear(dmgAmount,this);
            // if (health <= 0 && particleEffects != null && particleEffects.Length > 0)
            // {
            //     RpcExploding();
                // GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
                // Destroy(transform.parent.gameObject);
                // Destroy(explosion, 3f);
            // }
        }

        return coreDestroyed;

    }
    public void explodeServer()
    {
        if(particleEffects != null && particleEffects.Length > 0)
        {
            if (hitboxType == Hitbox.CoreHitbox)
            {
            RpcExplodingCore();
            }
            else if(hitboxType == Hitbox.FrontHitbox)
            {
             RpcExplodingFront();
            }
            else{
                RpcExploding();
            }
        }

    }
    [ClientRpc]
    void RpcExploding()
    {
                    GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
                    Destroy(transform.parent.gameObject);
                    Destroy(explosion, 3f);
    }

        [ClientRpc]
    void RpcExplodingCore()
    {
                GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
                explosion.transform.localScale += new Vector3(1f, 1f, 1f);
                Destroy(explosion, 3f);
                Destroy(transform.parent.gameObject);
    }
            [ClientRpc]
        void RpcExplodingFront()
    {
             GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
                explosion.transform.localScale -= new Vector3(1f,1f,1f);
                Destroy(explosion, 3f);
                Destroy(transform.parent.gameObject);
    }

}
