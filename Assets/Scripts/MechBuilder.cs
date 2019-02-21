using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MechBuilder : MonoBehaviour
{

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
        mech = Instantiate(mechPrefabs[selection]);
        toAttach = mech.GetComponent<MechComp>();
    }

    public void AddGun(int gun)
    {
        toAttach.AttachGunInOrder(Instantiate(guns[gun]));
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

    public void finishBuilding(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
