using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private CameraManager camManager;
    private List<KeyCode> weaponInputs;
    private GameObject playerMech;
    private List<GameObject> weapons;
    private ShootInput shootControls;
    private SmoothMouseLook lookControls;
    private MechMovement mechMovementControls;

    void Start()
    {
        camManager = GameObject.Find("Main Camera").GetComponent<CameraManager>();
        weaponInputs = new List<KeyCode> { KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };
        weapons = new List<GameObject>();
        shootControls = GetComponent<ShootInput>();
        lookControls = GetComponent<SmoothMouseLook>();
        mechMovementControls = GetComponent<MechMovement>();
    }

    void Update()
    {
        if (playerMech != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                DisableInput();
                camManager.FollowMech(playerMech);
            }

            for (int i = 0; i < weapons.Count; i++)
            {
                if (Input.GetKeyDown(weaponInputs[i]))
                {
                    DisableInput();
                    //camManager.AttachToWeapon(weapons[i]);
                }
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

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DisableInput();
            camManager.ResetPosition();
        }
    }

    public void EnableInput(GameObject currentObject)
    {
        if (currentObject.tag == "Mech")
        {
            mechMovementControls.mech = currentObject;
            mechMovementControls.enabled = true;
        }
        else if (currentObject.tag == "Weapon")
        {
            shootControls.currentGun = currentObject;
            shootControls.enabled = true;
            lookControls.enabled = true;
        }
    }

    public void DisableInput()
    {
        shootControls.enabled = false;
        lookControls.enabled = false;
        mechMovementControls.enabled = false;
    }
}
