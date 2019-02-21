﻿using System.Collections;
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
    private LineRenderer lineRenderer;

    public WeaponState(Transform playerTransform)
    {
        playerInput = playerTransform.GetComponent<PlayerInput>();

        lastKeyIndex = playerInput.lastKeyIndex;
        weapon = playerInput.weapons[lastKeyIndex];
        /*lineRenderer = weapon.GetComponent<LineRenderer>();
        ToggleLine();*/
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

    public void Update()
    {
        bool updateMouse = true;
        if (Input.GetButtonDown("Perspective1"))
        {
            updateMouse = false;
            weapon.localRotation = Quaternion.identity;
            shootInput.ToggleActive();
            //ToggleLine();
            playerInput.PrepareMechPerspec();
        }

        for (int i = 0; i < otherWeapons.Count; i++)
        {
            if (Input.GetButtonDown(otherWeaponInputs[i]))
            {
                updateMouse = false;
                weapon.localRotation = originalRotation;
                shootInput.ToggleActive();
                //ToggleLine();
                playerInput.PrepareWeaponPerspec(otherWeapons[i]);
            }
        }

        if (Input.GetButton("Fire1"))
        {
            shootInput.Shoot();
        }

        if (updateMouse)
        {
            mouseInput.Update();
        }
    }

    private void ToggleLine()
    {
        if (lineRenderer != null)
        {
            lineRenderer.enabled = !lineRenderer.enabled;
        }
    }

}
