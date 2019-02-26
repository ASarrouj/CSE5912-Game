using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TargetTakeDamage : NetworkBehaviour, IDamagable
{
    [SyncVar] public int health;
    public const int maxHealth=30;
    public void Damage(int dmgAmount)
    {
        health-=dmgAmount;
        if (health<0)
        {
            health=0;
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
