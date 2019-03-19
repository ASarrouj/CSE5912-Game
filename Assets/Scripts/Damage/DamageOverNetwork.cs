using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DamageOverNetwork : NetworkBehaviour
{
    private GameObject flameEffect;
    private PlayerHealth pHealth;

    // Start is called before the first frame update
    void Start()
    {
        pHealth = GetComponent<PlayerHealth>();
        flameEffect = Resources.Load("FlamesParticleEffect") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int dmg, string hb, NetworkIdentity mechID) {
        CmdDamagePlayer(dmg, hb, mechID);
    }

    [Command]
    void CmdDamagePlayer(int dmg, string hb, NetworkIdentity id) {
        NetworkServer.FindLocalObject(id.netId).GetComponent<DamageOverNetwork>().RpcHitboxTakeDamage(dmg, hb);
    }

    private Transform GetHitBox(GameObject mech, string hitboxName) {
        Transform hitbox = null;
        MechTakeDamage[] t = mech.GetComponentsInChildren<MechTakeDamage>();
        foreach (MechTakeDamage m in t) {
            if (m.gameObject.name == hitboxName) {
                hitbox = m.transform;
                break;
            }
        }
        return hitbox;
    }

    [ClientRpc]
    public void RpcHitboxTakeDamage(int dmgAmount, string hbName) {
        Transform hitbox = GetHitBox(gameObject, hbName);
        MechTakeDamage m = hitbox.GetComponent<MechTakeDamage>();
        if (gameObject == null) return;
        if (m.Invincible) return;
        if (hbName == "FrontHitbox") {
            pHealth.dmgFront(dmgAmount, m);
        } else if (hbName == "LeftHitbox") {
            pHealth.dmgLeft(dmgAmount, m);
        } else if (hbName == "RightHitbox") {
            Debug.Log("Right takes damage");
            pHealth.dmgRight(dmgAmount, m);
        } else if (hbName == "CoreHitbox") {
            Debug.Log("Core takes " + dmgAmount + " damage");
            pHealth.dmgCore(dmgAmount, m);
            GameObject flames = Instantiate(flameEffect, hitbox.transform.position, Quaternion.identity, hitbox.transform);
            flames.transform.localScale += new Vector3(1f, 1f, 1f);
        } else if (hbName == "RearHitbox") {
            Debug.Log("Rear takes damage");
            pHealth.dmgRear(dmgAmount, m);
        }

    }
}
