using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour
{
    // Start is called before the first frame update
    [SyncVar]public int coreHealth,rearHealth,leftHealth,rightHealth,frontHealth;

public bool dmgCore(int dmg, MechTakeDamage hitbox)
{
    coreHealth-=dmg;
    if(coreHealth<0)
    {
        hitbox.explodeServer();
        return true;
    }
    return false;
}
public void dmgFront(int dmg, MechTakeDamage hitbox)
{
    frontHealth-=dmg;
    
    if(frontHealth<0)
    {
        hitbox.explodeServer();
    }
}
public void dmgRear(int dmg, MechTakeDamage hitbox)
{
    rearHealth-=dmg;
        if(rearHealth<0)
    {
        hitbox.explodeServer();
    }
}
public void dmgLeft(int dmg, MechTakeDamage hitbox)
{
    leftHealth-=dmg;
        if(leftHealth<0)
    {
        hitbox.explodeServer();
    }
}
public void dmgRight(int dmg, MechTakeDamage hitbox)
{
    rightHealth-=dmg;
        if(rightHealth<0)
    {
        hitbox.explodeServer();
    }
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
