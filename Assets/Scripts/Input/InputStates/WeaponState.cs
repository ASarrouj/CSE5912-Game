﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponState : IInputState
{
    private Transform weapon;
    private PlayerInput playerInput;
    private List<string> otherWeaponInputs;
    private List<Transform> otherWeapons;
    private int lastPressIndex;
    private SmoothMouseLook mouseInput;
    private IWeapon shootInput;

    public WeaponState(Transform playerTransform, string lastKeyPress)
    {
        playerInput = playerTransform.GetComponent<PlayerInput>();

        lastPressIndex = playerInput.weaponInputs.IndexOf(lastKeyPress);
        weapon = playerInput.weapons[lastPressIndex];
        shootInput = weapon.GetComponent<IWeapon>();
        shootInput.ToggleActive();
        otherWeaponInputs = new List<string>(playerInput.weaponInputs);
        otherWeaponInputs.Remove(lastKeyPress);
        otherWeapons = new List<Transform>(playerInput.weapons);
        otherWeapons.RemoveAt(lastPressIndex);

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
                playerInput.lastKeyPress = otherWeaponInputs[i];
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
