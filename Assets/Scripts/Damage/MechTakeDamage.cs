using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechTakeDamage : MonoBehaviour, IDamagable
{
    public enum Hitbox { FrontHitbox, LeftHitbox, RightHitbox, CoreHitbox}
    public Hitbox hitboxType;
    public int health = 3;
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
        }

        if (hitboxType == Hitbox.LeftHitbox)
        {
            Debug.Log("Left takes damage");
        }
        if (hitboxType == Hitbox.RightHitbox)
        {
            Debug.Log("Right takes damage");
        }
        if (hitboxType == Hitbox.CoreHitbox)
        {
            Debug.Log("Core takes damage");
        }

        health -= dmgAmount;
        if (health <= 0) 
        {
            Destroy(transform.parent.gameObject);
        }

    }

}
