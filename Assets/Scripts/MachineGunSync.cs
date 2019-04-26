using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MachineGunSync : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public GameObject artilleryPrefab;
    public GameObject minePrefab;
    private ObjectPooler hitEffectPool;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("MatchStart", 1);
    }

    private void MatchStart() {
        hitEffectPool = GameObject.Find("MGImpactPool").GetComponent<ObjectPooler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (isServer)
        {
            RpcDrawMuzzleFlash(gameObject);
        }
        else
        {
            CmdDrawMuzzleFlash(gameObject);
        }
    }

    public void Hit(Vector3 hit_point, NetworkIdentity targetID)
    {
        if (isServer)
        {
            RpcDrawHit(hit_point);
        }
        else
        {
            CmdDrawHit(hit_point);
        }
    }

    [Command]
    void CmdDrawMuzzleFlash(GameObject mech)
    {
        RpcDrawMuzzleFlash(mech);
    }

    [ClientRpc]
    void RpcDrawMuzzleFlash(GameObject mech)
    {
        if (!isLocalPlayer)
        {
            StartCoroutine(mech.GetComponentInChildren<RayCastShoot>().ShotEffect());
        }
    }

    [Command]
    void CmdDrawHit(Vector3 hit_point)
    {
        RpcDrawHit(hit_point);
    }

    [ClientRpc]
    void RpcDrawHit(Vector3 hit_point)
    {
        if (!isLocalPlayer)
        {
            CmdSpawnBullet(hit_point);
        }
    }

    [Command]
    public void CmdSpawnBullet(Vector3 position) {
        GameObject newBullet = Instantiate(bulletPrefab);//.GetObject();
        newBullet.transform.position = position;
        newBullet.transform.rotation = Quaternion.identity;
        //newBullet.SetActive(true);

        NetworkServer.Spawn(newBullet);
    }

    [Command]
    public void CmdSpawnProjectile(Vector3 position, Quaternion rotation, Vector3 forward, float projectileForce, int scoreIndex)
    {
        //GameObject newProjectile = roundPool.GetObject();

        GameObject newProjectile = Instantiate(artilleryPrefab);

        
        newProjectile.transform.position = position + forward * .4f;
        newProjectile.transform.rotation = rotation;
        newProjectile.transform.Rotate(new Vector3(90, 0, 0));
        newProjectile.SetActive(true);
        newProjectile.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        newProjectile.GetComponent<Rigidbody>().AddForce(forward * projectileForce, ForceMode.Impulse);

        NetworkServer.Spawn(newProjectile);

        RpcSetScoreIndex(newProjectile, scoreIndex);
    }

    [ClientRpc]
    private void RpcSetScoreIndex(GameObject proj, int scoreIndex) {
        proj.GetComponent<ProjectileCollider>().scoreIndex = scoreIndex;
    }

    [Command]
    public void CmdArtilleryExplosion(Vector3 position)
    {
        ;
    }

    [Command]
    public void CmdDeployMine(Vector3 position)
    {
        GameObject mine = Instantiate(minePrefab);
        mine.transform.position = position;
        NetworkServer.Spawn(mine);
    }
}
