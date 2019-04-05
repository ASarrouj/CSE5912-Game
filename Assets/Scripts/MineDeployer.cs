using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDeployer : MonoBehaviour
{

    public GameObject minePrefab;
    private Transform gunEnd;
    private float reloadTime, timeStamp;

    // Start is called before the first frame update
    void Start()
    {
        reloadTime = 2;
        timeStamp = 0;
        gunEnd = transform.Find("GunEnd");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeployMine()
    {
        if (timeStamp <= Time.time)
        {
            RaycastHit hit;
            Physics.Raycast(gunEnd.position, Vector3.down, out hit);

            if (hit.collider.gameObject.layer == 8)
            {
                Instantiate(minePrefab, hit.point, Quaternion.identity);
            }

            timeStamp = Time.time + reloadTime;
        }
    }
}
