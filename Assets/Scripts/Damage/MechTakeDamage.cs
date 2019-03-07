using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MechTakeDamage : MonoBehaviour, IDamagable
{
    public bool Invincible;
    public enum Hitbox { FrontHitbox, LeftHitbox, RightHitbox, RearHitbox, CoreHitbox}
    public Hitbox hitboxType;
    private bool coreDestroyed;
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
        Destroy(transform.parent.gameObject);
        Destroy(explosion, 3f);
    }

    public void ExplodingCore() {
        GameObject explosion = Instantiate(particleEffects[1], transform.position, Quaternion.identity);
        explosion.transform.localScale += new Vector3(1f, 1f, 1f);
        explosion.AddComponent<NetworkIdentity>();
        NetworkServer.Spawn(explosion);
        Destroy(explosion, 3f);
        Destroy(transform.parent.gameObject);
    }

    public void ExplodingFront() {
        GameObject explosion = Instantiate(particleEffects[0], transform.position, Quaternion.identity);
        explosion.transform.localScale -= new Vector3(1f, 1f, 1f);
        explosion.AddComponent<NetworkIdentity>();
        NetworkServer.Spawn(explosion);
        Destroy(explosion, 3f);
        Destroy(transform.parent.gameObject);
    }

}
