using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairState : IInputState
{
    private PlayerInput playerInput;
    private List<string> otherSlotInputs;
    private ObjectPooler hitEffectPool;
    private List<Transform> otherSlots;
    private int maxSpeed, speedStep, lastKeyIndex;

    public RepairState(Transform playerTransform)
    {
        playerInput = playerTransform.GetComponent<PlayerInput>();
        lastKeyIndex = playerInput.lastKeyIndex;

        otherSlotInputs = new List<string>(playerInput.slotInputs);
        otherSlotInputs.RemoveAt(lastKeyIndex);
        otherSlots = new List<Transform>(playerInput.slots);
        otherSlots.RemoveAt(lastKeyIndex);

        hitEffectPool = GameObject.Find("MGImpactPool").GetComponent<ObjectPooler>();
    }

    public void Update(PlayerInput.InputType inputType)
    {
        playerInput.CheckUI();

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

                if (Input.GetButtonDown("RightClick"))
                {
                    Ray ray = playerInput.CreateRayFromMouseClick();
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 50f))
                    {
                        if (hit.collider.gameObject.tag == "Mech")
                        {
                            GameObject hitEffect = hitEffectPool.GetObject();
                            hitEffect.transform.position = hit.point;
                            hitEffect.transform.rotation = Quaternion.identity;
                            hitEffect.SetActive(true);

                            hit.collider.gameObject.GetComponent<MechTakeDamage>().Damage(-1);
                        }
                    }
                }

                if (Input.GetButtonDown("Left Click"))
                {
                    playerInput.SetDragOrigin();
                }

                if (Input.GetButton("Left Click"))
                {
                    playerInput.DragCamera();
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