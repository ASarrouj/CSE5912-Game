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

    public WeaponState(Transform playerTransform)
    {
        playerInput = playerTransform.GetComponent<PlayerInput>();

        lastKeyIndex = playerInput.lastKeyIndex;
        weapon = playerInput.weapons[lastKeyIndex];
        shootInput = weapon.GetComponent<IWeapon>();
        shootInput.ToggleActive();
        otherWeaponInputs = new List<string>(playerInput.weaponInputs);
        otherWeaponInputs.RemoveAt(lastKeyIndex);
        otherWeapons = new List<Transform>(playerInput.weapons);
        otherWeapons.RemoveAt(lastKeyIndex);

        mouseInput = new SmoothMouseLook(weapon);
        mouseInput.SetClamping(-60, 60, -30, 30);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Perspective1"))
        {
            shootInput.ToggleActive();
            playerInput.PrepareMechPerspec();
        }

        for (int i = 0; i < otherWeapons.Count; i++)
        {
            if (Input.GetButtonDown(otherWeaponInputs[i]))
            {
                shootInput.ToggleActive();
                playerInput.PrepareWeaponPerspec(otherWeapons[i]);
            }
        }

        if (Input.GetButton("Fire1"))
        {
            shootInput.Shoot();
        }

        mouseInput.Update();
    }
}
