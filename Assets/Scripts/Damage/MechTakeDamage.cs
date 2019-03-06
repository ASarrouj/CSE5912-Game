using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MechTakeDamage : MonoBehaviour, IDamagable
{
    public bool Invincible;
    public enum Hitbox { FrontHitbox, LeftHitbox, RightHitbox, RearHitbox, CoreHitbox}
    public Hitbox hitboxType;
    private bool coreDestroyed;
    public int health=30;
    [SerializeField] GameObject [] particleEffects;

    private DestroyMod destroyMod;

    // Start is called before the first frame update
    void Start()
    {
        coreDestroyed = false;
        destroyMod = transform.root.GetComponent<DestroyMod>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Damage(int dmgAmount) 
    {
        if (gameObject == null) return coreDestroyed;
        if (Invincible) return coreDestroyed;
        if (hitboxType == Hitbox.FrontHitbox)
        {
            Debug.Log("Front takes damage");
            transform.root.GetComponent<PlayerHealth>().dmgFront(dmgAmount,this);
            /*
             if (health <= 0 && particleEffects != null && particleEffects.Length > 0)
             {
                 destroyMod.RpcExplodingFront(gameObject, particleEffects[0]);
             }
             */
        }

        if (hitboxType == Hitbox.LeftHitbox)
        {
            Debug.Log("Left takes damage");

            transform.root.GetComponent<PlayerHealth>().dmgLeft(dmgAmount,this);
            /*
            if (health <= 0 && particleEffects != null && particleEffects.Length > 0)
            {
                 destroyMod.RpcExploding(gameObject, particleEffects[0]);
            }
            */
        }
        if (hitboxType == Hitbox.RightHitbox)
        {
            Debug.Log("Right takes damage");             
            transform.root.GetComponent<PlayerHealth>().dmgRight(dmgAmount,this);
            /*
            if (health <= 0 && particleEffects != null && particleEffects.Length > 0)
            {
                destroyMod.RpcExploding(gameObject, particleEffects[0]);
            }
            */


        }
        if (hitboxType == Hitbox.CoreHitbox)
        {
            Debug.Log("Core takes " + dmgAmount + " damage");
            
            coreDestroyed=transform.root.GetComponent<PlayerHealth>().dmgCore(dmgAmount,this);
            GameObject flames = Instantiate(particleEffects[1], transform.position, Quaternion.identity, transform);
            flames.transform.localScale += new Vector3(1f, 1f, 1f);
            /*
            if (health <= 0)
            {

                destroyMod.RpcExplodingCore(gameObject, particleEffects[0]);
            }
            */
        }

        if (hitboxType == Hitbox.RearHitbox)
        {
            Debug.Log("Rear takes damage");
            transform.root.GetComponent<PlayerHealth>().dmgRear(dmgAmount,this);
            /*
            if (health <= 0 && particleEffects != null && particleEffects.Length > 0)
            {
                destroyMod.RpcExploding(gameObject, particleEffects[0]);
            }
            */
        }

        return coreDestroyed;

    }
    public void explodeServer()
    {
        if(particleEffects != null && particleEffects.Length > 0)
        {
            destroyMod.Explode(gameObject, particleEffects[0], hitboxType);
        }

    }
    

}
