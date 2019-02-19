using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTakeDamage : MonoBehaviour, IDamagable
{
    public int health;
    public enum Hitbox { FrontHitbox, LeftHitbox, RightHitbox }
    public Hitbox hitboxType;
    public void Damage(int dmgAmount)
    {

            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            health -= dmgAmount;
            if (health < 0)
            {
                health = 0;
            }
        
    }
}
