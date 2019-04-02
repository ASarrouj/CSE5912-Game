using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechComp : MonoBehaviour
{
    public GameObject[] guns;
    public GameObject[] positions;
    public GameObject self;
    public int coreHealth;
    // these health values are for the different sections of the mech.
    public int[] healthValues;
    public float maxSpeed;
    public int moveSpeed, rotateSpeed;
    public int health;
    private int trackSpot;

    void Start()
    {
        moveSpeed = 0;
        rotateSpeed = 0;
        health = 100;
        trackSpot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttachGun(GameObject gun, int location)
    {
        if(location < guns.Length)
        {
            guns[location] = gun;
            guns[location].transform.SetParent(positions[location].transform, false);
        }
        else
        {
            print("This location is not available on the mech.");
        }
    }

    public void AttachGunInOrder(GameObject gun)
    {
        if (trackSpot < guns.Length)
        {
            guns[trackSpot] = Instantiate(gun, positions[trackSpot].transform, false);
            guns[trackSpot].transform.SetParent(positions[trackSpot].transform);
            if (guns[trackSpot].name.Equals("MachineGun"))
            {
                guns[trackSpot].GetComponent<RayCastShoot>().GetCameraAndScore();
            }
            trackSpot++;
        }
        else
        {
            print("This location is not available on the mech.");
        }
    }

    public void RemoveGun(int spot)
    {
        Destroy(guns[spot]);
    }

    public void RemoveGunInOrder()
    {
        Destroy(guns[trackSpot - 1]);
        trackSpot--;
    }

    public void EndOfMatch()
    {
        Destroy(self);
    }
}
