using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MechTakeDamage : NetworkBehaviour, IDamagable
{
    public bool Invincible;
    public enum Hitbox { FrontHitbox, LeftHitbox, RightHitbox, RearHitbox, CoreHitbox}
    public Hitbox hitboxType;
    public const int maxHealth=30;
    [SyncVar] public int health = 30;
    [SerializeField] GameObject [] particleEffects;
    // Start is called before the first frame update
    void Start()
    {
        if ((hitboxType == Hitbox.CoreHitbox)) {
            health = 100;
         }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int dmgAmount) 
    {
        if (Invincible) return;
        if (hitboxType == Hitbox.FrontHitbox)
        {
            Debug.Log("Front takes damage");
            Debug.Log("now has "+health);

            health -= dmgAmount;
            if (health <= 0 && particleEffects != null && particleEffects.Length > 0)
            {
                GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
                explosion.transform.localScale -= new Vector3(1f, 1f, 1f);
                Destroy(explosion, 3f);
                Destroy(transform.parent.gameObject);
            }
        }

        if (hitboxType == Hitbox.LeftHitbox)
        {
            Debug.Log("Left takes damage");

            health -= dmgAmount;
            if (health <= 0 && /*particleEffects != null &&*/particleEffects.Length > 0)
            {
                GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
                Destroy(explosion, 3f);
                Destroy(transform.parent.gameObject);
            }
        }
        if (hitboxType == Hitbox.RightHitbox)
        {
            Debug.Log("Right takes damage"); health -= dmgAmount;
                if (health <= 0 && /*particleEffects != null &&*/particleEffects.Length > 0)
                {
                    GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
                    Destroy(transform.parent.gameObject);
                    Destroy(explosion, 3f);
                }


        }
        if (hitboxType == Hitbox.CoreHitbox)
        {
            Debug.Log("Core takes " + dmgAmount + " damage");
            
            health -= dmgAmount;
            GameObject flames = Instantiate(particleEffects[1], transform.position, Quaternion.identity, transform);
            flames.transform.localScale += new Vector3(1f, 1f, 1f);
            if (health <= 0)
            {
                GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
                explosion.transform.localScale += new Vector3(2f, 2f, 2f);
                Destroy(explosion, 3f);
                Destroy(transform.parent.gameObject);
            }
        }

        if (hitboxType == Hitbox.RearHitbox)
        {
            Debug.Log("Rear takes damage");

            health -= dmgAmount;
            if (health <= 0 && particleEffects != null && particleEffects.Length > 0)
            {
                GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
                Destroy(explosion, 3f);
                Destroy(transform.parent.gameObject);
            }
        }

    }

}
