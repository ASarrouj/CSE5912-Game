using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    private GameObject currentHud, MechHud, WeaponHud;

    // Start is called before the first frame update
    void Start()
    {
        MechHud = GameObject.Find("MechHud");
        MechHud.SetActive(false);
        //WeaponHud = GameObject.Find();
        //WeaponHud.SetActive(false);
        currentHud = MechHud;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHud(GameObject targetObject)
    {
        if (targetObject.tag == "Mech")
        {
            currentHud = MechHud;
            currentHud.SetActive(true);
        }
        else if (targetObject.tag == "Weapon")
        {
            currentHud = WeaponHud;
            currentHud.SetActive(true);
        }
    }

    public void ClearHud()
    {
        currentHud.SetActive(false);
    }
}
