using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechComp : MonoBehaviour
{
    public GameObject[] guns;
    public GameObject[] positions;
    public GameObject self;
    public int moveSpeed, rotateSpeed;

    void Start()
    {
        moveSpeed = 0;
        rotateSpeed = 0;
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

    public void EndOfMatch()
    {
        Destroy(self);
    }
}
