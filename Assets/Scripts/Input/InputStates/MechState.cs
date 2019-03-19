using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechState : IInputState
{
    private PlayerInput playerInput;
    private List<string> slotInputs;
    private List<Transform> slots;
    private MechDriver driver;
    private int maxSpeed, speedStep;
    private bool ignoreStick;

    public MechState(Transform playerTransform)
    {
        playerInput = playerTransform.GetComponent<PlayerInput>();

        slotInputs = playerInput.slotInputs;
        slots = playerInput.slots;
        driver = playerInput.transform.GetComponent<MechDriver>();
    }

    public void Update(PlayerInput.InputType inputType)
    {
        if (Input.GetButtonDown("Escape")) {
            playerInput.ToggleMenu();
        }

        switch (inputType)
        {

            case PlayerInput.InputType.MouseKeyboard:

                for (int i = 0; i < slots.Count; i++)
                {
                    if (Input.GetButtonDown(slotInputs[i]))
                    {
                        playerInput.PrepareSlotPerspec(slots[i]);
                    }
                }

                if (Input.GetButtonDown("Forward"))
                {
                    driver.Accelerate();
                }
                if (Input.GetButtonDown("Backward"))
                {
                    driver.Decelerate();
                }
                if (Input.GetButton("Left"))
                {
                    driver.TurnLeft();
                }
                if (Input.GetButton("Right"))
                {
                    driver.TurnRight();
                }

                if (Input.GetButtonDown("Left Click"))
                {
                    playerInput.SetDragOrigin();
                }

                if (Input.GetButton("Left Click"))
                {
                    playerInput.DragCamera();
                }

                if (Input.GetButtonUp("Left Click"))
                {
                    playerInput.UnDragCamera();
                }

                break;

            case PlayerInput.InputType.Controller:

                float xPad = Input.GetAxis("Plus Pad X");
                float yPad = Input.GetAxis("Plus Pad Y");

                if (yPad > 0)
                { // front mod
                    playerInput.PrepareSlotPerspec(slots[0]);
                }
                else if (xPad < 0)
                { // left mod
                    playerInput.PrepareSlotPerspec(slots[1]);
                }
                else if (xPad > 0)
                { // right mod
                    //playerInput.PrepareWeaponPerspec(weapons[2]);
                }
                else if (yPad < 0)
                { // back mod
                    //playerInput.PrepareWeaponPerspec(weapons[3]);
                }

                float xAxis = Input.GetAxis("Left Stick X");
                float yAxis = Input.GetAxis("Left Stick Y");

                if (ignoreStick)
                {
                    if (yAxis > -0.1f && yAxis < 0.1f)
                    {
                        ignoreStick = false;
                    }
                }
                else
                {
                    if (yAxis > 0)
                    {
                        driver.Accelerate();
                        ignoreStick = true;
                    }
                    if (yAxis < 0)
                    {
                        driver.Decelerate();
                        ignoreStick = true;
                    }
                }

                if (xAxis < 0)
                {
                    driver.TurnLeft();
                }
                if (xAxis > 0)
                {
                    driver.TurnRight();
                }

                float xLook = Input.GetAxis("Right Stick X");
                if (xLook != 0.0f)
                {
                    playerInput.RotateCamera(xLook);
                }

                break;
        }
        
    }
}