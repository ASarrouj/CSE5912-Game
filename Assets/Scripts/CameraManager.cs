using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private Vector3 offsetPos, originalPos;
    private Quaternion offsetRot, originalRot;
    private float transitionTime;
    private const float transitionMaxTime = 0.5f;
    private const float DT = 0.01f;
    private bool interpolating;
    private GameObject targetObject;
    private UIManager uiManager;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        interpolating = false;
        transitionTime = 0.0f;
        uiManager = GameObject.Find("UI").GetComponent<UIManager>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interpolating)
            InterpolatePosition();
    }

    public void FollowMech(GameObject mechObject)
    {
        if (targetObject != mechObject)
        {
            PrepareInterp();
            offsetPos = -5 * mechObject.transform.forward + 3 * mechObject.transform.up; // Forward multiplier determines distance behind, up for above
            offsetRot = Quaternion.Euler(30, 0, 0); // X value determines level of downward tilt
            targetObject = mechObject;
        }
    }

    public void AttachToWeapon(GameObject weaponObject)
    {
        if (targetObject != weaponObject)
        {
            PrepareInterp();
            offsetPos = Vector3.zero;
            offsetRot = Quaternion.identity;
            targetObject = weaponObject;
        }
    }

    public void ResetPosition()
    {
        PrepareInterp();
        offsetPos = Vector3.zero;
        offsetRot = Quaternion.identity;
        targetObject = GameObject.Find("DebugCameraPos");
    }

    private void PrepareInterp()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
        interpolating = true;
        transitionTime = 0.0f;
        transform.parent = null;
        uiManager.ClearHud();
    }

    private void InterpolatePosition()
    {
        transform.position = Vector3.Lerp(originalPos, targetObject.transform.position + offsetPos, ComputeArcLengthFromTime(transitionTime));
        transform.rotation = Quaternion.Slerp(originalRot, targetObject.transform.rotation * offsetRot, ComputeArcLengthFromTime(transitionTime));

        if (transitionTime >= transitionMaxTime)
        {
            interpolating = false;
            transform.parent = targetObject.transform;
            uiManager.ShowHud(targetObject);
            inputManager.EnableInput(targetObject);
        }

        transitionTime += DT;
    }

    private float ComputeArcLengthFromTime(float time)
    {
        float normalizedTime = time / transitionMaxTime;
        return -2 * (float)Math.Pow(normalizedTime, 3) + 3 * (float)Math.Pow(normalizedTime, 2);
    }
}
