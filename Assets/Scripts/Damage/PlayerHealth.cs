using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class PlayerHealth : NetworkBehaviour
{
    // Start is called before the first frame update

    public int maxHealth;
    [SyncVar] public int coreHealth, rearHealth, leftHealth, rightHealth, frontHealth;
    [SerializeField] HealthBar[] healthbars;

    public bool isPlayer = true;

    public bool coreDestroyed = false;

    private PlayerStatus status;

    private void Awake()
    {
        status = gameObject.GetComponent<PlayerStatus>();
    }

    public bool dmgCore(int dmg, MechTakeDamage hitbox)
    {
        Debug.Log("Core takes " + dmg + " damage");
        coreHealth -= dmg;
        if (coreHealth < 0)
        {
            coreHealth = 0;
            //eSync.Explode("CoreHitbox", MechTakeDamage.Hitbox.CoreHitbox);
            hitbox.Exploding();
            status.Destroyed = true;
        }
        coreDestroyed = (coreHealth == 0);
        if (isPlayer) healthbars[0].setHealthBar((float)coreHealth / (float)maxHealth);
        return coreDestroyed;
    }

    public void dmgFront(int dmg, MechTakeDamage hitbox)
    {
        Debug.Log("Front takes damage");
        frontHealth -= dmg;
        if (frontHealth < 0)
        {
            frontHealth = 0;
            //eSync.Explode("FrontHitbox", MechTakeDamage.Hitbox.FrontHitbox);
            hitbox.Exploding();
        }
        if (isPlayer) healthbars[1].setHealthBar((float)frontHealth / (float)maxHealth);
    }

    public void dmgRear(int dmg, MechTakeDamage hitbox)
    {
        Debug.Log("Rear takes damage");
        rearHealth -= dmg;
        if (rearHealth < 0)
        {
            rearHealth = 0;
            hitbox.Exploding();
        }
        if (isPlayer) healthbars[4].setHealthBar((float)rearHealth / (float)maxHealth);
    }

    public void dmgLeft(int dmg, MechTakeDamage hitbox)
    {
        Debug.Log("Left takes damage");
        leftHealth -= dmg;

        if (leftHealth < 0)
        {
            leftHealth = 0;
            hitbox.Exploding();
        }
        if (isPlayer) healthbars[2].setHealthBar((float)leftHealth / (float)maxHealth);
    }

    public void dmgRight(int dmg, MechTakeDamage hitbox)
    {
        rightHealth -= dmg;
        if (rightHealth < 0)
        {
            rightHealth = 0;
            hitbox.Exploding();
        }
        if (isPlayer) healthbars[3].setHealthBar((float)rightHealth / (float)maxHealth);
    }

    // Testing health bars
    public void healCore(int heal)
    {
        coreHealth += heal;
        if (coreHealth > maxHealth)
        {
            coreHealth = maxHealth;
        }
    }
    public void healFront(int heal)
    {
        frontHealth += heal;
        if (frontHealth > maxHealth)
        {
            frontHealth = maxHealth;
        }
    }
    public void healRear(int heal)
    {
        rearHealth += heal;
        if (rearHealth > maxHealth)
        {
            rearHealth = maxHealth;
        }
    }
    public void healLeft(int heal)
    {
        leftHealth += heal;
        if (leftHealth > maxHealth)
        {
            leftHealth = maxHealth;
        }
    }
    public void healRight(int heal)
    {
        rightHealth += heal;
        if (rightHealth > maxHealth)
        {
            rightHealth = maxHealth;
        }
    }




    void Update()
    {
        if (isPlayer)
        {
            healthbars[0].setHealthBar((float)coreHealth / (float)maxHealth);
            healthbars[1].setHealthBar((float)frontHealth / (float)maxHealth);
            healthbars[2].setHealthBar((float)leftHealth / (float)maxHealth);
            healthbars[3].setHealthBar((float)rightHealth / (float)maxHealth);
            healthbars[4].setHealthBar((float)rearHealth / (float)maxHealth);
        }
    }
}
