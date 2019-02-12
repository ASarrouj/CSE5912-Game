using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponState : IInputState
{
    private Transform weapon;
    private PlayerInput playerInput;
    private List<KeyCode> otherWeaponInputs;
    private List<Transform> otherWeapons;
    private int lastPressIndex;
    private SmoothMouseLook mouseInput;

    public WeaponState(Transform playerTransform, KeyCode lastKeyPress)
    {
        playerInput = playerTransform.GetComponent<PlayerInput>();

        lastPressIndex = playerInput.weaponInputs.IndexOf(lastKeyPress);
        otherWeaponInputs = new List<KeyCode>(playerInput.weaponInputs);
        otherWeaponInputs.Remove(lastKeyPress);
        otherWeapons = new List<Transform>(playerInput.weapons);
        otherWeapons.RemoveAt(lastPressIndex);

        mouseInput = new SmoothMouseLook(playerInput.weapons[lastPressIndex]);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerInput.PrepareMechPerspec();
        }

        for (int i = 0; i < otherWeapons.Count; i++)
        {
            if (Input.GetKeyDown(otherWeaponInputs[i]))
            {
                playerInput.lastKeyPress = otherWeaponInputs[i];
                playerInput.PrepareWeaponPerspec(otherWeapons[i]);
            }
        }

        mouseInput.Update();
    }
}
