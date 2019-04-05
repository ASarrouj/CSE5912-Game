using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDeployer : MonoBehaviour
{

    public GameObject minePrefab;
    private Transform deployer;
    private float reloadTime, timeStamp;

    // Start is called before the first frame update
    void Start()
    {
        reloadTime = 2;
        timeStamp = 0;
        deployer = transform.Find("Gun3");
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
            Physics.Raycast(deployer.position, Vector3.down, out hit);

            if (hit.collider.gameObject.tag == "Ground")
            {
                Instantiate(minePrefab, hit.transform.position, Quaternion.identity);
            }

            timeStamp = Time.time + reloadTime;
        }
    }
}
