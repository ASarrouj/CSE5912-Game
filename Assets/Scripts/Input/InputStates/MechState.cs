using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechState : IInputState
{
    private PlayerInput playerInput;
    private List<KeyCode> weaponInputs;
    private List<Transform> weapons;
    private MoveTest mechStats;
    private int maxSpeed, speedStep;

    public MechState(Transform playerTransform)
    {
        playerInput = playerTransform.GetComponent<PlayerInput>();

        weaponInputs = playerInput.weaponInputs;
        weapons = playerInput.weapons;
        mechStats = playerInput.transform.GetComponent<MoveTest>();

        speedStep = 4;
        maxSpeed = 12;
    }

    public void Update()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (Input.GetKeyDown(weaponInputs[i]))
            {
                playerInput.lastKeyPress = weaponInputs[i];
                playerInput.PrepareWeaponPerspec(weapons[i]);
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && mechStats.moveSpeed < maxSpeed)
        {
            mechStats.moveSpeed += speedStep;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && mechStats.moveSpeed > -4)
        {
            mechStats.moveSpeed -= speedStep;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && mechStats.moveSpeed != 0 && mechStats.rotateSpeed >= -30 && mechStats.rotateSpeed <= 30)
        {
            if (mechStats.moveSpeed > 0 && mechStats.rotateSpeed > -30)
            {
                mechStats.rotateSpeed -= 5;
            }
            else if (mechStats.moveSpeed < 0 && mechStats.rotateSpeed < 30)
            {
                mechStats.rotateSpeed += 5;
            }

        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && mechStats.moveSpeed != 0 && mechStats.rotateSpeed <= 30 && mechStats.rotateSpeed >= -30)
        {
            if (mechStats.moveSpeed > 0 && mechStats.rotateSpeed < 30)
            {
                mechStats.rotateSpeed += 5;
            }
            else if (mechStats.moveSpeed < 0 && mechStats.rotateSpeed > -30)
            {
                mechStats.rotateSpeed -= 5;
            }
        }

        if (mechStats.moveSpeed == 0)
        {
            mechStats.rotateSpeed = 0;
        }
    }
}