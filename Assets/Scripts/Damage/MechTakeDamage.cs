﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechTakeDamage : MonoBehaviour, IDamagable
{
    public enum Hitbox { FrontHitbox, LeftHitbox, RightHitbox, CoreHitbox}
    public Hitbox hitboxType;
    public int health = 30;
    [SerializeField] GameObject [] particleEffects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int dmgAmount) 
    {
        if (hitboxType == Hitbox.FrontHitbox)
        {
            Debug.Log("Front takes damage");

            health -= dmgAmount;
            if (health <= 0)
            {
                GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
                explosion.transform.localScale -= new Vector3(1f,1f,1f);
                Destroy(explosion, 3f);
                Destroy(transform.parent.gameObject);
            }
        }

        if (hitboxType == Hitbox.LeftHitbox)
        {
            Debug.Log("Left takes damage");

            health -= dmgAmount;
            if (health <= 0)
            {
                GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
                Destroy(explosion, 3f);
                Destroy(transform.parent.gameObject);
            }
        }
        if (hitboxType == Hitbox.RightHitbox)
        {
            Debug.Log("Right takes damage"); health -= dmgAmount;
                if (health <= 0)
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
                explosion.transform.localScale += new Vector3(1f, 1f, 1f);
                Destroy(explosion, 3f);
                Destroy(transform.parent.gameObject);
            }
        }


    }

}
