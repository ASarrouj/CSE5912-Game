using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponState : IInputState
{
    private Transform weapon;
    private PlayerInput playerInput;
    private List<string> otherWeaponInputs;
    private List<Transform> otherWeapons;
    private int lastKeyIndex;
    private SmoothMouseLook mouseInput;
    private IWeapon shootInput;
    private Quaternion originalRotation;
    private GameObject weaponUI;

    public WeaponState(Transform playerTransform)
    {
        playerInput = playerTransform.GetComponent<PlayerInput>();

        lastKeyIndex = playerInput.lastKeyIndex;
        weapon = playerInput.weapons[lastKeyIndex];
        weaponUI = weapon.GetChild(0).Find("WeaponUI").gameObject;
        ToggleGunUI();
        shootInput = weapon.GetComponent<IWeapon>();
        shootInput.ToggleActive();
        otherWeaponInputs = new List<string>(playerInput.weaponInputs);
        otherWeaponInputs.RemoveAt(lastKeyIndex);
        otherWeapons = new List<Transform>(playerInput.weapons);
        otherWeapons.RemoveAt(lastKeyIndex);
        originalRotation = weapon.localRotation;

        mouseInput = new SmoothMouseLook(weapon);
        mouseInput.SetClamping(-60, 60, -30, 30);
    }

    public void Update(PlayerInput.InputType inputType)
    {
        if (Input.GetButtonDown("Escape")) {
            playerInput.ToggleMenu();
        }

        bool updateMouse = true;

        switch (inputType) {

            case PlayerInput.InputType.MouseKeyboard:

                if (Input.GetButtonDown("Perspective1")) {
                    updateMouse = false;
                    weapon.localRotation = Quaternion.identity;
                    shootInput.ToggleActive();
                    ToggleGunUI();
                    playerInput.PrepareMechPerspec();
                }
                for (int i = 0; i < otherWeapons.Count; i++) {
                    if (Input.GetButtonDown(otherWeaponInputs[i])) {
                        updateMouse = false;
                        weapon.localRotation = originalRotation;
                        ToggleGunUI();
                        playerInput.PrepareWeaponPerspec(otherWeapons[i]);
                        shootInput.ToggleActive();
                    }
                }         
                break;

            case PlayerInput.InputType.Controller:

                if (Input.GetButtonDown("Cancel")) {
                    updateMouse = false;
                    weapon.localRotation = Quaternion.identity;
                    shootInput.ToggleActive();
                    ToggleGunUI();
                    playerInput.PrepareMechPerspec();
                }
                for (int i = 0; i < otherWeapons.Count; i++) {
                    if (Input.GetButtonDown(otherWeaponInputs[i])) {
                        updateMouse = false;
                        weapon.localRotation = originalRotation;
                        ToggleGunUI();
                        playerInput.PrepareWeaponPerspec(otherWeapons[i]);
                        shootInput.ToggleActive();
                    }
                }

                float xPad = Input.GetAxis("Plus Pad X");
                float yPad = Input.GetAxis("Plus Pad Y");

                int select = -1;

                if (yPad > 0) { // front mod
                    select = 0;
                } else if (xPad < 0) { // left mod
                    select = 1;
                } else if (xPad > 0) { // right mod
                    // select = 2;
                } else if (yPad < 0) { // back mod
                    // select = 3;
                }

                if (select >= 0 && select != lastKeyIndex) {
                    updateMouse = false;
                    weapon.localRotation = originalRotation;
                    ToggleGunUI();
                    playerInput.PrepareWeaponPerspec(playerInput.weapons[select]);
                    shootInput.ToggleActive();
                }

                break;
        }

        if (Input.GetButton("Fire1"))
        {
            shootInput.Shoot();
        }

        if (updateMouse) {
            mouseInput.Update(inputType);
        }

    }

    private void ToggleGunUI()
    {
        weaponUI.SetActive(!weaponUI.activeSelf);
    }

}
