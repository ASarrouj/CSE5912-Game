using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour
{
    private CameraManager camManager;
    private IInputState inputState;
    public List<KeyCode> weaponInputs;
    private GameObject playerMech;
    public List<GameObject> weapons;
    private ShootInput shootControls;
    private SmoothMouseLook lookControls;
    private MechMovement mechMovementControls;

    void Start()
    {
        camManager = transform.Find("PlayerCamera").GetComponent<CameraManager>();
        camManager.SetInput(this);
        PrepareMechPerspec();
        weaponInputs = new List<KeyCode> { KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };
        weapons = new List<GameObject>();
        //shootControls = GetComponent<ShootInput>();
        //lookControls = GetComponent<SmoothMouseLook>();
        //mechMovementControls = GetComponent<MechMovement>();
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            inputState.Update();
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                DisableInput();
                camManager.FollowMech(playerMech);
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

        /*
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            DisableInput();
            camManager.ResetPosition();
        }
        */
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

    public void PrepareMechPerspec()
    {
        DisableInput();
        camManager.FollowMech(transform.gameObject);
    }

    public void PrepareWeaponPerspec(GameObject weapon)
    {
        DisableInput();
        camManager.AttachToWeapon(weapon);
    }

    public void FinalizeInterp(GameObject currentObject)
    {
        if (currentObject.tag == "Player")
        {
            inputState = new MechState(transform);
        }
        else if (currentObject.tag == "Weapon")
        {
            shootControls.currentGun = currentObject.gameObject;
            shootControls.enabled = true;
            lookControls.enabled = true;
        }
    }

    public void DisableInput()
    {
        inputState = new DisabledState();
    }
}
