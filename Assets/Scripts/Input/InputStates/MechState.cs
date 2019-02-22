using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechState : IInputState
{
    private PlayerInput playerInput;
    private List<string> weaponInputs;
    private List<Transform> weapons;
    private MechMouseLook mouseLook;
    private MoveTest mechStats;
    private int maxSpeed, speedStep;

    public MechState(Transform playerTransform)
    {
        playerInput = playerTransform.GetComponent<PlayerInput>();

        weaponInputs = playerInput.weaponInputs;
        weapons = playerInput.weapons;
        mechStats = playerInput.transform.GetComponent<MoveTest>();
        //mouseLook = new MechMouseLook(playerInput.transform.Find("PlayerCamera").GetComponent<Camera>());

        speedStep = 4;
        maxSpeed = 12;
    }

    public void Update()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (Input.GetButtonDown(weaponInputs[i]))
            {
                playerInput.PrepareWeaponPerspec(weapons[i]);
            }
        }

        if (Input.GetButtonDown("Forward") && mechStats.moveSpeed < maxSpeed)
        {
            mechStats.moveSpeed += speedStep;
        }
        if (Input.GetButtonDown("Backward") && mechStats.moveSpeed > -4)
        {
            mechStats.moveSpeed -= speedStep;
        }
        if (Input.GetButton("Left") && mechStats.moveSpeed != 0 && mechStats.rotateSpeed >= -30 && mechStats.rotateSpeed <= 30)
        {
            if (mechStats.moveSpeed > 0 && mechStats.rotateSpeed > -30)
            {
                mechStats.rotateSpeed -= 1f;
            }
            else if (mechStats.moveSpeed < 0 && mechStats.rotateSpeed < 30)
            {
                mechStats.rotateSpeed += 1f;
            }

        }
        if (Input.GetButton("Right") && mechStats.moveSpeed != 0 && mechStats.rotateSpeed <= 30 && mechStats.rotateSpeed >= -30)
        {
            if (mechStats.moveSpeed > 0 && mechStats.rotateSpeed < 30)
            {
                mechStats.rotateSpeed += 1f;
            }
            else if (mechStats.moveSpeed < 0 && mechStats.rotateSpeed > -30)
            {
                mechStats.rotateSpeed -= 1f;
            }
        }

        if (mechStats.moveSpeed == 0 || Input.GetButtonDown("Shift"))
        {
            mechStats.rotateSpeed = 0;
        }

        if (Input.GetButton("Fire1"))
        {
            //mouseLook.Update();
        }


    }
}