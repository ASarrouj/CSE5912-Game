using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DamageOverNetwork : NetworkBehaviour
{
    [SyncVar]
    public bool invincible = false;
    public GameObject barrier;
    private WaitForSeconds BarrierLength = new WaitForSeconds(2f);
    private GameObject flameEffect;
    private PlayerHealth pHealth;
    private float shieldEndTime;
    private float nextShieldTime;
    private float shieldCoolDown = 10f;
    private float shieldLastingTime = 2f;

    private ScoreManager scoreManager;
    private int playerIndex;

    // Start is called before the first frame update
    void Start()
    {
        pHealth = GetComponent<PlayerHealth>();
        flameEffect = Resources.Load("FlamesParticleEffect") as GameObject;
        Invoke("MatchStart", 4);
        playerIndex = GetComponent<Prototype.NetworkLobby.PlayerStatus>().index;
    }

    private void MatchStart() {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincible && Time.time > shieldEndTime)
        {
            invincible = false;
        }
    }

    public void turnInvincible()
    {
        if (Time.time > nextShieldTime)
        {
            shieldEndTime = Time.time + shieldLastingTime;
            nextShieldTime = Time.time + shieldCoolDown;
            invincible = true;
            createBarrier();
            StartCoroutine(BarrierEffect());
        }

    }

    public void DamagePlayer(int dmg, string hb, NetworkIdentity targetID, int scoreIndex) {
        if (isServer) NetworkServer.FindLocalObject(targetID.netId).GetComponent<DamageOverNetwork>().RpcHitboxTakeDamage(dmg, hb, scoreIndex);
        else CmdDamagePlayer(dmg, hb, targetID, scoreIndex);
    }

    public void HealPlayer(int heal, string hb, NetworkIdentity mechID)
    {
        CmdHealPlayer(heal, hb, mechID);
    }

    [ClientRpc]
    public void RpcHitboxTakeDamage(int dmgAmount, string hbName, int scoreIndex) {
        Transform hitbox = GetHitBox(gameObject, hbName);
        if (hitbox == null) return;
        MechTakeDamage m = hitbox.GetComponent<MechTakeDamage>();
        if (gameObject == null) return;
        if (invincible) return;
        if (m.Invincible) return;

        int scoreVal = 0;

        if (hbName == "FrontHitbox") {
            scoreVal = pHealth.dmgFront(dmgAmount, m);
        } else if (hbName == "LeftHitbox") {
            scoreVal = pHealth.dmgLeft(dmgAmount, m);
        } else if (hbName == "RightHitbox") {
            Debug.Log("Right takes damage");
            scoreVal = pHealth.dmgRight(dmgAmount, m);
        } else if (hbName == "CoreHitbox") {
            Debug.Log("Core takes " + dmgAmount + " damage");
            scoreVal = pHealth.dmgCore(dmgAmount, m);
            GameObject flames = Instantiate(flameEffect, hitbox.transform.position, Quaternion.identity, hitbox.transform);
            flames.transform.localScale += new Vector3(1f, 1f, 1f);
        } else if (hbName == "RearHitbox") {
            Debug.Log("Rear takes damage");
            scoreVal = pHealth.dmgRear(dmgAmount, m);
        }

        if (scoreVal > 0 && scoreIndex >= 0) {
            CmdAddScore(scoreIndex, scoreVal);
        }
    }

    [Command]
    private void CmdAddScore(int index, int sc) {
        scoreManager.AddScore(index, sc);
    }

    [ClientRpc]
    public void RpcHitboxRepairDamage(int healAmount, string hbName)
    {
        Transform hitbox = GetHitBox(gameObject, hbName);
        if (gameObject == null) return;
        if (hbName == "FrontHitbox")
        {
            pHealth.healFront(healAmount);
        }
        else if (hbName == "LeftHitbox")
        {
            pHealth.healLeft(healAmount);
        }
        else if (hbName == "RightHitbox")
        {
            Debug.Log("Right heals");
            pHealth.healRight(healAmount);
        }
        else if (hbName == "CoreHitbox")
        {
            Debug.Log("Core heals " + healAmount + " health");
            pHealth.healCore(healAmount);
        }
        else if (hbName == "RearHitbox")
        {
            Debug.Log("Rear heals");
            pHealth.healRear(healAmount);
        }



    }


    [Command]
    void CmdDamagePlayer(int dmg, string hb, NetworkIdentity targetID, int scoreIndex)
    {
        GameObject target = NetworkServer.FindLocalObject(targetID.netId);
        target.GetComponent<DamageOverNetwork>().RpcHitboxTakeDamage(dmg, hb, scoreIndex);
    }

    [Command]
    void CmdHealPlayer(int dmg, string hb, NetworkIdentity id)
    {
        NetworkServer.FindLocalObject(id.netId).GetComponent<DamageOverNetwork>().RpcHitboxRepairDamage(dmg, hb);
    }

    private Transform GetHitBox(GameObject mech, string hitboxName)
    {
        Transform hitbox = null;
        MechTakeDamage[] t = mech.GetComponentsInChildren<MechTakeDamage>();
        foreach (MechTakeDamage m in t)
        {
            if (m.gameObject.name == hitboxName)
            {
                hitbox = m.transform;
                break;
            }
        }
        return hitbox;
    }

    public void createBarrier()
    {
        if (isServer)
        {
            RpcDrawBarrier(gameObject);
        }
        else
        {
            CmdDrawBarrier(gameObject);
        }
    }

    [Command]
    void CmdDrawBarrier(GameObject mech)
    {
        RpcDrawBarrier(mech);
    }

    [ClientRpc]
    void RpcDrawBarrier(GameObject mech)
    {
        if (!isLocalPlayer)
        {
            StartCoroutine(BarrierEffect());
        }
    }
    public IEnumerator BarrierEffect()
    {
        //lineRenderer.enabled = true;
        barrier.SetActive(true);

        yield return BarrierLength;
        //lineRenderer.enabled = false;
        barrier.SetActive(false);
    }
}
