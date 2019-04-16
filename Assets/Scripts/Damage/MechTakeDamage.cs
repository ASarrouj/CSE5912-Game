using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MechTakeDamage : MonoBehaviour, IDamagable
{
    public bool Invincible;
    public enum Hitbox { FrontHitbox, LeftHitbox, RightHitbox, RearHitbox, CoreHitbox}
    public Hitbox hitboxType;
    public bool coreDestroyed;
    public int health=30;
    [SerializeField] GameObject [] particleEffects;

    private PlayerHealth pHealth;
    //private DestroyMod destroyMod;

    // Start is called before the first frame update
    void Start()
    {
        coreDestroyed = false;
        pHealth = transform.root.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // instead of this call DamageOverNetwork.DamagePlayer()
    public bool Damage(int dmgAmount) 
    {
        return false;
    }

    public void Exploding() {
        GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
        explosion.AddComponent<NetworkIdentity>();
        NetworkServer.Spawn(explosion);
        ForceCameraSwitch();
        Destroy(explosion, 3f);
        //DestroyNetChild();
        RpcDisableParent();
        //Destroy(transform.parent.gameObject);
    }

    public void ExplodingCore() {
        coreDestroyed = true;
        GameObject explosion = Instantiate(particleEffects[1], transform.position, Quaternion.identity);
        explosion.transform.localScale += new Vector3(1f, 1f, 1f);
        explosion.AddComponent<NetworkIdentity>();
        NetworkServer.Spawn(explosion);
        ForceCameraSwitch();
        Destroy(explosion, 3f);
        //DestroyNetChild();
        RpcDisableParent();
        //Destroy(transform.parent.gameObject);
    }

    public void ExplodingFront() {
        GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
        explosion.transform.localScale -= new Vector3(1f, 1f, 1f);
        explosion.AddComponent<NetworkIdentity>();
        NetworkServer.Spawn(explosion);
        ForceCameraSwitch();
        Destroy(explosion, 3f);
        //DestroyNetChild();
        RpcDisableParent();
        //Destroy(transform.parent.gameObject);
    }

    private void DestroyNetChild() {
        Transform wep = null;
        foreach (Transform c in transform.parent) {
            if (c.tag == "Weapon") {
                wep = c;
                break;
            }
        }
        if (wep == null) {
            return;
        }
        NetworkTransformChild[] netChildren = transform.root.GetComponents<NetworkTransformChild>();
        foreach (NetworkTransformChild c in netChildren) {
            if (c.target == wep) {
                c.enabled = false;
                break;
            }
        }
    }

    private void ForceCameraSwitch() {
        PlayerInput PI = transform.root.GetComponent<PlayerInput>();
        PI.HandleDeadSlot(transform.name);
    }

    [ClientRpc]
    void RpcDisableParent()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
