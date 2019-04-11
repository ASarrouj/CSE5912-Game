using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MechComp : NetworkBehaviour
{
    public GameObject[] guns;
    public GameObject[] positions;
    public GameObject self;
    public PlayerInput selfInput;
    public Camera viewCam;
    public Text scoreStuff;
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

    public void EndOfMatch()
    {
        Destroy(self);
    }
}
