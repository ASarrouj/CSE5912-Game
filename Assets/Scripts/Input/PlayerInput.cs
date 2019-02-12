using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour
{
    private CameraManager camManager;
    private IInputState inputState;
    public List<KeyCode> weaponInputs;
    public KeyCode lastKeyPress;
    private Transform playerMech;
    public List<Transform> weapons;
    private ShootInput shootControls;
    private SmoothMouseLook lookControls;
    private MechMovement mechMovementControls;

    void Start()
    {
        camManager = transform.Find("PlayerCamera").GetComponent<CameraManager>();
        camManager.SetInput(this);

        // Begin game by interpolating to mech
        PrepareMechPerspec();

        weaponInputs = new List<KeyCode> { KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4 };
        weapons = new List<Transform>();
        playerMech = transform.Find("NewMechWithGuns");
        weapons.Add(playerMech.Find("FrontGun").GetChild(0));
        weapons.Add(playerMech.Find("LeftGun").GetChild(0));
        //shootControls = GetComponent<ShootInput>();
        //lookControls = GetComponent<SmoothMouseLook>();
        //mechMovementControls = GetComponent<MechMovement>();
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            inputState.Update();
        }
    }

    public void PrepareMechPerspec()
    {
        DisableInput();
        camManager.FollowMech(transform);
    }

    public void PrepareWeaponPerspec(Transform weapon)
    {
        DisableInput();
        camManager.AttachToWeapon(weapon);
    }

    public void EnableInput(Transform currentTransform)
    {
        if (currentTransform.tag == "Player")
        {
            inputState = new MechState(transform);
        }
        else if (currentTransform.tag == "Weapon")
        {
            inputState = new WeaponState(transform, lastKeyPress);
        }
    }

    public void DisableInput()
    {
        inputState = new DisabledState();
    }
}
