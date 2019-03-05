using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour
{
    private CameraManager camManager;
    private UIManager uiManager;
    private IInputState inputState;
    public List<string> weaponInputs;
    public int lastKeyIndex;
    private Transform playerMech;
    public List<Transform> weapons;

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
            weaponInputs = new List<string> { "Perspective2", "Perspective3", "Perspective4"};
            weapons = new List<Transform>();
            playerMech = transform.Find("NewMechWithGuns");
            weapons.Add(playerMech.Find("FrontGun").GetChild(0));
            weapons.Add(playerMech.Find("LeftGun").GetChild(0));

            camManager = transform.Find("PlayerCamera").GetComponent<CameraManager>();
            camManager.SetInput(this);

            uiManager = transform.Find("UI").GetComponent<UIManager>();
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
        uiManager.ChangeSlotHighlight(-1);
        camManager.FollowMech(transform);
    }

    public void PrepareWeaponPerspec(Transform weapon)
    {
        DisableInput();
        lastKeyIndex = weapons.IndexOf(weapon);
        uiManager.ChangeSlotHighlight(lastKeyIndex);
        camManager.AttachToWeapon(weapon);
    }

    public void FinalizePerspective(Transform currentTransform)
    {
        if (currentTransform.tag == "Player")
        {
            inputState = new MechState(transform);
            uiManager.MechUI();
        }
        else if (currentTransform.tag == "Weapon")
        {
            inputState = new WeaponState(transform);
            uiManager.WeaponUI();
        }
    }

    public void DisableInput()
    {
        inputState = new DisabledState();
    }

    public void ToggleMenu() {
        uiManager.MenuUI();
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
