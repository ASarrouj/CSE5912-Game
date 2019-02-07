using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private CameraManager camManager;
    private List<KeyCode> weaponInputs;
    private GameObject playerMech;
    private List<GameObject> weapons;

    void Start()
    {
        camManager = GameObject.Find("Main Camera").GetComponent<CameraManager>();
        weaponInputs = new List<KeyCode> { KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };
        weapons = new List<GameObject>();
    }

    void Update()
    {
        if (playerMech != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                camManager.FollowMech(playerMech);
            }

            for (int i = 0; i < weapons.Count; i++)
            {
                if (Input.GetKeyDown(weaponInputs[i]))
                    camManager.AttachToWeapon(weapons[i]);
            }
        }
        else
        {
            if ((playerMech = GameObject.FindWithTag("Mech")) != null)
            {
                foreach (Transform child in transform)
                {
                    if (child.gameObject.tag == "Weapon")
                        weapons.Add(child.gameObject);
                }
            }
        }
    }

    public void EnableInput()
    {

    }

    public void DisableInput()
    {
        
    }
}
