using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechComp : MonoBehaviour
{
    public GameObject[] guns;
    public GameObject[] positions;
    public GameObject self;
    private int[] healthValues;
    public int moveSpeed, rotateSpeed;
    public int health;
    private int trackSpot;

    void Start()
    {
        moveSpeed = 0;
        rotateSpeed = 0;
        health = 100;
        trackSpot = 0;
        healthValues = new int[positions.Length];
        int track = 0;
        while (track < healthValues.Length)
        {
            healthValues[track] = 100;
            track++;
        }
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
            guns[trackSpot] = gun;
            guns[trackSpot].transform.SetParent(positions[trackSpot].transform, false);
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
