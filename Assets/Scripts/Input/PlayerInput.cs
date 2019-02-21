using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour
{
    private CameraManager camManager;
    private UIManager uiManager;
    private IInputState inputState;
    public List<string> weaponInputs;
    public int lastKeyIndex;
    private Transform playerMech;
    public List<Transform> weapons;

    void Start()
    {
        if (!isLocalPlayer)
        {
            transform.Find("UI").gameObject.SetActive(false);
            transform.Find("PlayerCamera").gameObject.SetActive(false);
        }
        else
        {
            weaponInputs = new List<string> { "Perspective2", "Perspective3", "Perspective4"};
            weapons = new List<Transform>();
            playerMech = transform.Find("NewMechWithGuns");
            weapons.Add(playerMech.Find("FrontGun").GetChild(0));
            weapons.Add(playerMech.Find("LeftGun").GetChild(0));

            camManager = transform.Find("PlayerCamera").GetComponent<CameraManager>();
            camManager.SetInput(this);

            uiManager = transform.Find("UI").GetComponent<UIManager>();
            uiManager.CreateSwitcherUI(this);

            // Begin game by interpolating to mech
            PrepareMechPerspec();
        }
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
        uiManager.ChangeSlotHighlight(-1);
        camManager.FollowMech(transform);
    }

    public void PrepareWeaponPerspec(Transform weapon)
    {
        DisableInput();
        lastKeyIndex = weapons.IndexOf(weapon);
        uiManager.ChangeSlotHighlight(lastKeyIndex);
        camManager.AttachToWeapon(weapon);
    }

    public void FinalizePerspective(Transform currentTransform)
    {
        if (currentTransform.tag == "Player")
        {
            inputState = new MechState(transform);
            uiManager.MechUI();
        }
        else if (currentTransform.tag == "Weapon")
        {
            inputState = new WeaponState(transform);
            uiManager.WeaponUI();
        }
    }

    public void DisableInput()
    {
        inputState = new DisabledState();
    }
}
