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
    public GameObject mechScreen;
    private GameObject currentScreen;
    

    private GameObject mech;
    private MechComp toAttach;

    private void Start()
    {
        currentScreen = mechScreen;
    }

    public void createMech(int selection)
    {
        mech = Instantiate(mechPrefabs[selection], playerPrefab.transform);
        toAttach = mech.GetComponent<MechComp>();
        mech.transform.SetParent(playerPrefab.transform);
    }

    public void AddGun(int gun)
    {
        toAttach.AttachGunInOrder(guns[gun]);
    }

    public void moveToGunScreen(int screen)
    {
        currentScreen.SetActive(false);
        partScreens[screen].SetActive(true);
        currentScreen = partScreens[screen];
    }

    public void backToMechScreen(int current)
    {
        partScreens[current].SetActive(false);
        mechScreen.SetActive(true);
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
