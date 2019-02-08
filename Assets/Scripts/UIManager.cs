using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private GameObject MechHUD, SwitcherHUD, NavHUD;


    // Start is called before the first frame update
    void Start()
    {
        MechHUD = GameObject.Find("MechHUD");
        SwitcherHUD = GameObject.Find("SwitcherHUD");
        NavHUD = GameObject.Find("NavHUD");

        MechHUD.SetActive(false);
        SwitcherHUD.SetActive(false);
        NavHUD.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void navigationUI()
    {
        MechHUD.SetActive(true);
        SwitcherHUD.SetActive(true);
        NavHUD.SetActive(true);
    }

    public void gunUI()
    {
        MechHUD.SetActive(true);
        SwitcherHUD.SetActive(true);
        NavHUD.SetActive(false);
    }

    public void noUI()
    {
        MechHUD.SetActive(false);
        SwitcherHUD.SetActive(false);
        NavHUD.SetActive(false);
    }
}
