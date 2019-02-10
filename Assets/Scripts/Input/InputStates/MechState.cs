using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechState : IInputState
{
    private PlayerInput playerInput;
    private List<KeyCode> weaponInputs;
    private List<GameObject> weapons;
    private MoveTest mechStats;

    public MechState(Transform playerTransform)
    {
        playerInput = playerTransform.GetComponent<PlayerInput>();

        weaponInputs = playerInput.weaponInputs;
        weapons = playerInput.weapons;
        mechStats = playerInput.transform.GetComponent<MoveTest>();
    }

    public void Update()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (Input.GetKeyDown(weaponInputs[i]))
            {
                playerInput.PrepareWeaponPerspec(weapons[i]);
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && mechStats.moveSpeed < 3)
        {
            mechStats.moveSpeed++;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && mechStats.moveSpeed > -1)
        {
            mechStats.moveSpeed--;
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