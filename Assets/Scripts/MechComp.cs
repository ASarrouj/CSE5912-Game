using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechComp : MonoBehaviour
{
    public GunInterface[] guns;
    public Transform[] positions;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttachGun(GunInterface gun, int location)
    {
        if(location < guns.Length)
        {
            guns[location] = gun;
            guns[location].transform.position = positions[location].transform.position;
            guns[location].transform.rotation = positions[location].transform.rotation;
        }
        else
        {
            print("This location is not available on the mech.");
        }
    }
}
