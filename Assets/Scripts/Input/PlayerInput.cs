using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour
{

    public GameObject repairEffect;

    private CameraManager camManager;
    private UIManager uiManager;
    private IInputState inputState;
    public List<string> slotInputs;
    public int lastKeyIndex;
    private Transform playerMech;
    public List<Transform> slots;
    private Dictionary<string, int> hitboxMap;
    public Texture2D blowtorchSprite;

    public enum InputType
    {
        MouseKeyboard,
        Controller
    };

    private InputType input = InputType.MouseKeyboard;

    void Start()
    {
        if (!isLocalPlayer)
        {
            transform.Find("UI").gameObject.SetActive(false);
            transform.Find("PlayerCamera").gameObject.SetActive(false);
        }
        else
        {
            slotInputs = new List<string> { "Perspective2", "Perspective3", "Perspective4", "Perspective5"};
            slots = new List<Transform>();
            playerMech = transform.Find("NewMechWithSlots");
            camManager = transform.Find("PlayerCamera").GetComponent<CameraManager>();
            camManager.SetInput(this);

            uiManager = transform.Find("UI").GetComponent<UIManager>();
            hitboxMap = new Dictionary<string, int>
            {
                {"FrontHitbox", 0},
                {"LeftHitbox", 1},
                {"RightHitbox", 2},
                {"RearHitbox", 3},
                {"CoreHitbox", 4}
            };

            DisableInput();
        }
    }

    public void addGun(int trackSpot)
    {
        if (trackSpot == 0)
        {
            slots.Add(playerMech.Find("FrontGun").GetChild(1));
            camManager.MoveToNextBuildSlot(transform);
        }
        else if (trackSpot == 1)
        {
            slots.Add(playerMech.Find("LeftGun").GetChild(1));
            camManager.MoveToNextBuildSlot(transform);
        }
        else if (trackSpot == 2)
        {
            slots.Add(playerMech.Find("RightGun").GetChild(1));
            camManager.MoveToNextBuildSlot(transform);
        }
        else if (trackSpot == 3)
        {
            slots.Add(playerMech.Find("RearGun").GetChild(1));
            uiManager.CreateSwitcherUI(this);
            // Begin game by interpolating to mech
            PrepareMechPerspec();
        }
    }

    void Update()
    {
        if (isLocalPlayer)
        {
             inputState.Update(input);
        }
    }

    void OnGUI() {
        switch (input) {
            case InputType.MouseKeyboard:
                if (IsControllerInput()) {
                    input = InputType.Controller;
                    Debug.Log("Gamepad being used");
                }
                break;
            case InputType.Controller:
                if (IsMouseKeyboard()) {
                    input = InputType.MouseKeyboard;
                    Debug.Log("Mouse & Keyboard being used");
                }
                break;
        }
    }

    public void PrepareMechPerspec()
    {
        DisableInput();
        lastKeyIndex = -1;
        uiManager.ChangeSlotHighlight(lastKeyIndex);
        camManager.FollowMech(transform);
    }

    public void PrepareSlotPerspec(Transform slot)
    {
        if (slot.parent.gameObject.activeSelf == true)
        {
            if (slot.tag == "Weapon")
            {
                DisableInput();
                lastKeyIndex = slots.IndexOf(slot);
                uiManager.ChangeSlotHighlight(lastKeyIndex);
                camManager.AttachToWeapon(slot);
            }
            else if (slot.tag == "RepairTool" || slot.tag == "MineDeployer")
            {
                DisableInput();
                lastKeyIndex = slots.IndexOf(slot);
                uiManager.ChangeSlotHighlight(lastKeyIndex);
                camManager.FollowMech(transform);
            }
        }
    }

    public void SetInputState()
    {
        if (lastKeyIndex == -1)
        {
            inputState = new MechState(transform);
            uiManager.MechUI();
        }
        else if (slots[lastKeyIndex].tag == "Weapon")
        {
            inputState = new WeaponState(transform);
            uiManager.WeaponUI();
        }
        else if (slots[lastKeyIndex].tag == "RepairTool")
        {
            inputState = new RepairState(transform, repairEffect);
            ToggleRepairCursor();
            uiManager.MechUI();
        }
        else if (slots[lastKeyIndex].tag == "MineDeployer")
        {
            inputState = new MineState(transform);
            uiManager.MechUI();
        }
        else
        {
            inputState = new MechState(transform);
            uiManager.MechUI();
        }
    }

    public void DisableInput()
    {
        inputState = new DisabledState();
    }

    public void CheckUI() {
        if (Input.GetButtonDown("Tab")) {
            uiManager.ScoreboardUI(true);
        } else if (Input.GetButtonUp("Tab")) {
            uiManager.ScoreboardUI(false);
        } else if (Input.GetButtonDown("Escape")) {
            uiManager.MenuUI();
        }
    }

    public void DragCamera()
    {
        camManager.DragCamera();
    }

    public void UnDragCamera()
    {
        camManager.UnDragCamera();
    }

    public void SetDragOrigin()
    {
        camManager.SetDragOrigin();
    }

    public void RotateCamera(float xInput) {
        camManager.RotateCamera(xInput);
    }

    public Ray CreateRayFromMouseClick()
    {
        return camManager.CreateRayFromMouseClick();
    }

    public void ToggleRepairCursor()
    {
        if (Cursor.visible)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            Cursor.visible = false;
        }
        else
        {
            Cursor.SetCursor(blowtorchSprite, Vector2.zero, CursorMode.Auto);
            Cursor.visible = true;
        }
    }


    public void HandleDeadSlot(string hitboxName)
    {
        if (lastKeyIndex == hitboxMap[hitboxName])
        {
            Cursor.visible = false;
            PrepareMechPerspec();
        }
        uiManager.DisableSlot(hitboxMap[hitboxName] + 1);
    }

    private bool IsMouseKeyboard() {
        if (Event.current.isKey || Event.current.isMouse) {
            return true;
        }
        if (Input.GetAxis("Mouse X") != 0.0f || Input.GetAxis("Mouse Y") != 0.0f) {
            return true;
        }
        return false;
    }

    private bool IsControllerInput() {
        if (Input.GetKey(KeyCode.Joystick1Button0) ||
           Input.GetKey(KeyCode.Joystick1Button1) ||
           Input.GetKey(KeyCode.Joystick1Button2) ||
           Input.GetKey(KeyCode.Joystick1Button3) ||
           Input.GetKey(KeyCode.Joystick1Button4) ||
           Input.GetKey(KeyCode.Joystick1Button5) ||
           Input.GetKey(KeyCode.Joystick1Button6) ||
           Input.GetKey(KeyCode.Joystick1Button7) ||
           Input.GetKey(KeyCode.Joystick1Button8) ||
           Input.GetKey(KeyCode.Joystick1Button9) ||
           Input.GetKey(KeyCode.Joystick1Button10) ||
           Input.GetKey(KeyCode.Joystick1Button11) ||
           Input.GetKey(KeyCode.Joystick1Button12) ||
           Input.GetKey(KeyCode.Joystick1Button13) ||
           Input.GetKey(KeyCode.Joystick1Button14) ||
           Input.GetKey(KeyCode.Joystick1Button15) ||
           Input.GetKey(KeyCode.Joystick1Button16) ||
           Input.GetKey(KeyCode.Joystick1Button17) ||
           Input.GetKey(KeyCode.Joystick1Button18) ||
           Input.GetKey(KeyCode.Joystick1Button19)) {
            return true;
        }

        if (Input.GetAxis("Left Stick X") != 0.0f ||
           Input.GetAxis("Left Stick Y") != 0.0f ||
           Input.GetAxis("Right Stick X") != 0.0f ||
           Input.GetAxis("Right Stick Y") != 0.0f) {
            return true;
        }

        return false;
    }
}
