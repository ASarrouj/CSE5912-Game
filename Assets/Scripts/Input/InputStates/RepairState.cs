using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RepairState : IInputState
{
    private GameObject repairEffect;
    private PlayerInput playerInput;
    private List<string> otherSlotInputs;
    private ObjectPooler hitEffectPool;
    private List<Transform> otherSlots;
    private MechDriver driver;
    private int maxSpeed, speedStep, lastKeyIndex;

    public RepairState(Transform playerTransform, GameObject effect)
    {
        playerInput = playerTransform.GetComponent<PlayerInput>();
        lastKeyIndex = playerInput.lastKeyIndex;

        otherSlotInputs = new List<string>(playerInput.slotInputs);
        otherSlotInputs.RemoveAt(lastKeyIndex);
        otherSlots = new List<Transform>(playerInput.slots);
        otherSlots.RemoveAt(lastKeyIndex);
        driver = playerInput.transform.GetComponent<MechDriver>();

        hitEffectPool = GameObject.Find("MGImpactPool").GetComponent<ObjectPooler>();
        repairEffect = GameObject.Find("YellowSparklerMissile");
        if (repairEffect == null)
        {
            repairEffect = GameObject.Instantiate(effect);
            repairEffect.SetActive(false);
        }
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

                if (Input.GetButton("RightClick"))
                {
                    Ray ray = playerInput.CreateRayFromMouseClick();
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 50f))
                    {
                        if (hit.collider.gameObject.tag == "Mech")
                        {
                            repairEffect.transform.position = hit.point;
                            repairEffect.transform.rotation = Quaternion.identity;
                            repairEffect.SetActive(true);

                            Debug.Log(hit.collider.gameObject.name);
                            hit.collider.gameObject.GetComponentInParent<DamageOverNetwork>().HealPlayer(1, hit.collider.gameObject.name, playerInput.transform.GetComponent<NetworkIdentity>());
                        }
                        else
                        {
                            repairEffect.SetActive(false);
                        }
                    }
                }
                else
                {
                    repairEffect.SetActive(false);
                }

                if (Input.GetButtonDown("Left Click"))
                {
                    playerInput.SetDragOrigin();
                }

                if (Input.GetButton("Left Click"))
                {
                    playerInput.DragCamera();
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
                if (Input.GetButtonDown("Jump"))
                {
                    driver.jump();

                }

                if (Input.GetButtonDown("Shield"))
                {
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