using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MechBuilder : MonoBehaviour
{

    public GameObject self;
    public GameObject playerUI;
    public GameObject playerPrefab;
    public GameObject[] mechPrefabs;
    public GameObject[] partScreens;
    public GameObject[] guns;
    private GameObject currentScreen;
    private GunAttacher gunAttacher;
    private CameraManager camManager;

    public GameObject mech;
    private MechComp toAttach;

    private int trackSpot;

    private void Start()
    {
        gunAttacher = gameObject.GetComponentInParent<GunAttacher>();
        trackSpot = 0;

        if (!transform.root.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            self.SetActive(false);
        }
        else
        {
            currentScreen = partScreens[0];
            toAttach = mech.GetComponent<MechComp>();
            camManager = transform.parent.Find("PlayerCamera").GetComponent<CameraManager>();
            camManager.SetBuilderLocations(new Vector3[4] { new Vector3(5f, 1f, 0f), new Vector3(0f, 1f, -5f), new Vector3(0f, 1f, 5f), new Vector3(-10.5f, 1f, 0f) },
                new Quaternion[4] { Quaternion.Euler(0, 180, 0), Quaternion.Euler(0, 90, 0), Quaternion.Euler(0, -90, 0), Quaternion.identity });
            camManager.MoveToNextBuildSlot(playerPrefab.transform);
            playerPrefab.GetComponent<MechDriver>().findColliders();
        }
    }

    public void createMech(int selection)
    {
        //leftover in case mech creation is involving picking mechs again
    }

    public void AddGun(int gun)
    {
        AttachGunInOrder(guns[gun]);
    }

    public void AddAntiGravity(int gun)
    {
        AttachGunInOrder(guns[gun]);
        playerPrefab.GetComponent<MechDriver>().canJump = true;
    }

    public void AddShield(int gun)
    {
        AttachGunInOrder(guns[gun]);
        playerPrefab.GetComponent<MechDriver>().canShield = true;
    }

    public void moveToGunScreen(int screen)
    {
        currentScreen.SetActive(false);
        partScreens[screen].SetActive(true);
        currentScreen = partScreens[screen];
    }

    public void removeLastGun()
    {
        RemoveGunInOrder();
    }

    public void finishBuilding()
    {
        Cursor.visible = false;
        playerUI.SetActive(true);
        self.SetActive(false);
    }


    public void AttachGun(GameObject gun, int location)
    {
        if (location < toAttach.guns.Length)
        {
            toAttach.guns[location] = gun;
            toAttach.guns[location].transform.SetParent(toAttach.positions[location].transform, false);
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
            toAttach.guns[trackSpot] = Instantiate(gun, toAttach.positions[trackSpot].transform, false);
            gunAttacher.CmdAttachGun(toAttach.guns[trackSpot], toAttach.positions[trackSpot].name);
            toAttach.selfInput.addGun(trackSpot);

            if (toAttach.guns[trackSpot].name.Equals("MachineGun(Clone)"))
            {
                toAttach.guns[trackSpot].GetComponent<RayCastShoot>().GetCameraAndScore(toAttach.viewCam);
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
        Destroy(toAttach.guns[spot]);
    }

    public void RemoveGunInOrder()
    {
        Destroy(toAttach.guns[trackSpot - 1]);
        trackSpot--;
    }
}

