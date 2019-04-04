using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MechBuilder : MonoBehaviour
{

    public GameObject self;
    public GameObject playerUI;
    public GameObject playerPrefab;
    public GameObject[] mechPrefabs;
    public GameObject[] partScreens;
    public GameObject[] guns;
    private GameObject currentScreen;
    

    public GameObject mech;
    private MechComp toAttach;

    private void Start()
    {
        currentScreen = partScreens[0];
        toAttach = mech.GetComponent<MechComp>();
        playerPrefab.GetComponent<MechDriver>().findColliders();
    }

    public void createMech(int selection)
    {
        //leftover in case mech creation is involving picking mechs again
    }

    public void AddGun(int gun)
    {
        toAttach.AttachGunInOrder(guns[gun]);
    }

    public void AddAntiGravity(int gun)
    {
        toAttach.AttachGunInOrder(guns[gun]);
        playerPrefab.GetComponent<MechDriver>().canJump = true;
    }

    public void moveToGunScreen(int screen)
    {
        currentScreen.SetActive(false);
        partScreens[screen].SetActive(true);
        currentScreen = partScreens[screen];
    }

    public void removeLastGun()
    {
        toAttach.RemoveGunInOrder();
    }

    public void finishBuilding()
    {
        Cursor.visible = false;
        playerUI.SetActive(true);
        self.SetActive(false);
    }
}
