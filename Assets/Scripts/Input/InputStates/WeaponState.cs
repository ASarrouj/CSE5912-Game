using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponState : IInputState
{

    private PlayerInput playerInput;
    private List<KeyCode> weaponInputs;
    private List<Transform> weapons;
    private int lastPressIndex;

    public WeaponState(Transform playerTransform, KeyCode lastKeyPress)
    {
        playerInput = playerTransform.GetComponent<PlayerInput>();

        lastPressIndex = playerInput.weaponInputs.IndexOf(lastKeyPress);
        weaponInputs = new List<KeyCode>(playerInput.weaponInputs);
        weaponInputs.Remove(lastKeyPress);
        weapons = new List<Transform>(playerInput.weapons);
        weapons.RemoveAt(lastPressIndex);
    }

    public void Update()
    {
        Debug.Log("weapon");
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerInput.PrepareMechPerspec();
        }

        for (int i = 0; i < weapons.Count; i++)
        {
            if (Input.GetKeyDown(weaponInputs[i]))
            {
                playerInput.lastKeyPress = weaponInputs[i];
                playerInput.PrepareWeaponPerspec(weapons[i]);
            }
        }
    }
}
