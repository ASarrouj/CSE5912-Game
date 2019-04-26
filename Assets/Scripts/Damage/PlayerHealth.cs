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

    //public bool coreDestroyed = false;

    public Dictionary<string, bool> hbDestroyed;

    private PlayerStatus status;

    private void Awake()
    {
        status = gameObject.GetComponent<PlayerStatus>();
        hbDestroyed = new Dictionary<string, bool>();
        hbDestroyed.Add("CoreHitbox", false);
        hbDestroyed.Add("FrontHitbox", false);
        hbDestroyed.Add("RearHitbox", false);
        hbDestroyed.Add("LeftHitbox", false);
        hbDestroyed.Add("RightHitbox", false);
    }

    public int dmgCore(int dmg, MechTakeDamage hitbox)
    {
        Debug.Log("Core takes " + dmg + " damage");
        int score = 0;
        coreHealth -= dmg;
        if (coreHealth <= 0)
        {
            coreHealth = 0;
            //eSync.Explode("CoreHitbox", MechTakeDamage.Hitbox.CoreHitbox);
            hitbox.Exploding();
            if(isPlayer) status.Destroyed = true;
            hbDestroyed["CoreHitbox"] = true;
            score = 300;
        } 
        if (isPlayer) healthbars[0].setHealthBar((float)coreHealth / (float)maxHealth);
        return score;
    }

    public int dmgFront(int dmg, MechTakeDamage hitbox)
    {
        Debug.Log("Front takes damage");
        int score = 0;
        frontHealth -= dmg;
        if (frontHealth <= 0)
        {
            frontHealth = 0;
            //eSync.Explode("FrontHitbox", MechTakeDamage.Hitbox.FrontHitbox);
            hitbox.Exploding();
            hbDestroyed["FrontHitbox"] = true;
            score = 100;
        }
        if (isPlayer) healthbars[1].setHealthBar((float)frontHealth / (float)maxHealth);
        return score;
    }

    public int dmgRear(int dmg, MechTakeDamage hitbox)
    {
        Debug.Log("Rear takes damage");
        int score = 0;
        rearHealth -= dmg;
        if (rearHealth <= 0)
        {
            rearHealth = 0;
            hitbox.Exploding();
            hbDestroyed["RearHitbox"] = true;
            score = 100;
        }
        if (isPlayer) healthbars[4].setHealthBar((float)rearHealth / (float)maxHealth);
        return score;
    }

    public int dmgLeft(int dmg, MechTakeDamage hitbox)
    {
        Debug.Log("Left takes damage");
        int score = 0;
        leftHealth -= dmg;

        if (leftHealth <= 0)
        {
            leftHealth = 0;
            hitbox.Exploding();
            hbDestroyed["LeftHitbox"] = true;
            score = 100;
        }
        if (isPlayer) healthbars[2].setHealthBar((float)leftHealth / (float)maxHealth);
        return score;
    }

    public int dmgRight(int dmg, MechTakeDamage hitbox)
    {
        int score = 0;
        rightHealth -= dmg;
        if (rightHealth <= 0)
        {
            rightHealth = 0;
            hitbox.Exploding();
            hbDestroyed["RightHitbox"] = true;
            score = 100;
        }
        if (isPlayer) healthbars[3].setHealthBar((float)rightHealth / (float)maxHealth);
        return score;
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
