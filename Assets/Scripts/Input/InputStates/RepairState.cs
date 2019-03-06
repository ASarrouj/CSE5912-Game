using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairState : IInputState
{
    private PlayerInput playerInput;
    private List<string> otherWeaponInputs;
    private ObjectPooler hitEffectPool;
    private List<Transform> otherWeapons;
    private int maxSpeed, speedStep, lastKeyIndex;

    public RepairState(Transform playerTransform)
    {
        playerInput = playerTransform.GetComponent<PlayerInput>();
        lastKeyIndex = playerInput.lastKeyIndex;

        otherWeaponInputs = new List<string>(playerInput.weaponInputs);
        otherWeaponInputs.RemoveAt(lastKeyIndex);
        otherWeapons = new List<Transform>(playerInput.weapons);
        otherWeapons.RemoveAt(lastKeyIndex);

        hitEffectPool = GameObject.Find("MGImpactPool").GetComponent<ObjectPooler>();
    }

    public void Update(PlayerInput.InputType inputType)
    {
        if (Input.GetButtonDown("Escape"))
        {
            playerInput.ToggleMenu();
        }

        if (Input.GetButtonDown("Fire1"))
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
                }
            }
        }

        switch (inputType)
        {

            case PlayerInput.InputType.MouseKeyboard:

                if (Input.GetButtonDown("Perspective1"))
                {
                    playerInput.PrepareMechPerspec();
                }
                for (int i = 0; i < otherWeapons.Count; i++)
                {
                    if (Input.GetButtonDown(otherWeaponInputs[i]))
                    {
                        playerInput.PrepareWeaponPerspec(otherWeapons[i]);
                    }
                }

                break;

            case PlayerInput.InputType.Controller:

                if (Input.GetButtonDown("Cancel"))
                {
                    playerInput.PrepareMechPerspec();
                }
                for (int i = 0; i < otherWeapons.Count; i++)
                {
                    if (Input.GetButtonDown(otherWeaponInputs[i]))
                    {
                        playerInput.PrepareWeaponPerspec(otherWeapons[i]);
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
                    playerInput.PrepareWeaponPerspec(playerInput.weapons[select]);
                }

                break;
        }
    }
}