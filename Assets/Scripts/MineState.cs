using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineState : IInputState
{
    private Transform minePrefab;
    private PlayerInput playerInput;
    private List<string> otherSlotInputs;
    private List<Transform> otherSlots;
    private int lastKeyIndex;
    private MineDeployer mineDeployer;
    private MechDriver driver;

    public MineState(Transform playerTransform)
    {
        playerInput = playerTransform.GetComponent<PlayerInput>();

        lastKeyIndex = playerInput.lastKeyIndex;
        minePrefab = playerInput.slots[lastKeyIndex];
        otherSlotInputs = new List<string>(playerInput.slotInputs);
        otherSlotInputs.RemoveAt(lastKeyIndex);
        otherSlots = new List<Transform>(playerInput.slots);
        otherSlots.RemoveAt(lastKeyIndex);
        mineDeployer = minePrefab.GetComponent<MineDeployer>();
        driver = playerInput.transform.GetComponent<MechDriver>();
    }

    public void Update(PlayerInput.InputType inputType)
    {
        if (Input.GetButtonDown("Escape"))
        {
            //playerInput.ToggleMenu();
        }


        if (Input.GetButton("Fire1"))
        {
            mineDeployer.DeployMine();
        }

        switch (inputType)
        {

            case PlayerInput.InputType.MouseKeyboard:

                if (Input.GetButtonDown("Perspective1"))
                {
                    playerInput.PrepareMechPerspec();
                }
                for (int i = 0; i < otherSlots.Count; i++)
                {
                    if (Input.GetButtonDown(otherSlotInputs[i]))
                    {
                        playerInput.PrepareSlotPerspec(otherSlots[i]);
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
                if (Input.GetButton("ResetRotation"))
                {
                    driver.ResetRotation();
                }
                if (Input.GetButtonDown("Space"))
                {
                    driver.jump();
                    driver.shield();
                }

                break;

            case PlayerInput.InputType.Controller:

                if (Input.GetButtonDown("Cancel"))
                {
                    playerInput.PrepareMechPerspec();
                }
                for (int i = 0; i < otherSlots.Count; i++)
                {
                    if (Input.GetButtonDown(otherSlotInputs[i]))
                    {
                        playerInput.PrepareSlotPerspec(otherSlots[i]);
                    }
                }

                float xPad = Input.GetAxis("Plus Pad X");
                float yPad = Input.GetAxis("Plus Pad Y");

                int select = -1;

                if (yPad > 0)
                { // front mod
                    select = 0;
                }
                else if (xPad < 0)
                { // left mod
                    select = 1;
                }
                else if (xPad > 0)
                { // right mod
                    // select = 2;
                }
                else if (yPad < 0)
                { // back mod
                    // select = 3;
                }

                if (select >= 0 && select != lastKeyIndex)
                {
                    playerInput.PrepareSlotPerspec(playerInput.slots[select]);
                }

                break;
        }
    }
}
