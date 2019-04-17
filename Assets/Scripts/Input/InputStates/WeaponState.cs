using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponState : IInputState
{
    private Transform weapon;
    private PlayerInput playerInput;
    private List<string> otherSlotInputs;
    private List<Transform> otherSlots;
    private int lastKeyIndex;
    private SmoothMouseLook mouseInput;
    private IWeapon shootInput;
    private Quaternion originalRotation;
    private MechDriver driver;

    public WeaponState(Transform playerTransform)
    {
        playerInput = playerTransform.GetComponent<PlayerInput>();

        lastKeyIndex = playerInput.lastKeyIndex;
        weapon = playerInput.slots[lastKeyIndex];
        shootInput = weapon.GetComponent<IWeapon>();
        shootInput.ToggleActive();
        otherSlotInputs = new List<string>(playerInput.slotInputs);
        otherSlotInputs.RemoveAt(lastKeyIndex);
        otherSlots = new List<Transform>(playerInput.slots);
        otherSlots.RemoveAt(lastKeyIndex);
        originalRotation = weapon.localRotation;
        driver = playerInput.transform.GetComponent<MechDriver>();

        mouseInput = new SmoothMouseLook(weapon);
        mouseInput.SetClamping(-60, 60, -30, 30);
    }

    public void Update(PlayerInput.InputType inputType)
    {
        playerInput.CheckUI();

        bool updateMouse = true;

        switch (inputType) {

            case PlayerInput.InputType.MouseKeyboard:

                if (Input.GetButtonDown("Perspective1")) {
                    updateMouse = false;
                    weapon.localRotation = Quaternion.identity;
                    shootInput.ToggleActive();
                    playerInput.PrepareMechPerspec();
                }
                for (int i = 0; i < otherSlots.Count; i++) {
                    if (Input.GetButtonDown(otherSlotInputs[i])) {
                        updateMouse = false;
                        weapon.localRotation = originalRotation;
                        playerInput.PrepareSlotPerspec(otherSlots[i]);
                        shootInput.ToggleActive();
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

                if (Input.GetButtonDown("Cancel")) {
                    updateMouse = false;
                    weapon.localRotation = Quaternion.identity;
                    shootInput.ToggleActive();
                    playerInput.PrepareMechPerspec();
                }
                for (int i = 0; i < otherSlots.Count; i++) {
                    if (Input.GetButtonDown(otherSlotInputs[i])) {
                        updateMouse = false;
                        weapon.localRotation = originalRotation;
                        playerInput.PrepareSlotPerspec(otherSlots[i]);
                        shootInput.ToggleActive();
                    }
                }

                float xPad = Input.GetAxis("Plus Pad X");
                float yPad = Input.GetAxis("Plus Pad Y");

                int select = -1;

                if (yPad > 0) { // front mod
                    select = 0;
                } else if (xPad < 0) { // left mod
                    select = 1;
                } else if (xPad > 0) { // right mod
                    // select = 2;
                } else if (yPad < 0) { // back mod
                    // select = 3;
                }

                if (select >= 0 && select != lastKeyIndex) {
                    updateMouse = false;
                    weapon.localRotation = originalRotation;
                    playerInput.PrepareSlotPerspec(playerInput.slots[select]);
                    shootInput.ToggleActive();
                }

                break;
        }

        if (Input.GetButton("Fire1"))
        {
            shootInput.Shoot();
        }

        if (updateMouse) {
            mouseInput.Update(inputType);
        }

    }

    private void ToggleGunUI()
    {
    }

}
