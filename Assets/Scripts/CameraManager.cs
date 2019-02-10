using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private Vector3 offsetPos, originalPos;
    private Quaternion offsetRot, originalRot;
    private float transitionTime;
    private const float transitionMaxTime = 0.5f;
    private const float DT = 0.005f;
    private bool interpolating;
    private GameObject targetObject;
    private UIManager uiManager;
    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        interpolating = false;
        transitionTime = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (interpolating)
            InterpolatePosition();
    }

    public void SetInput(PlayerInput input)
    {
        playerInput = input;
    }

    public void FollowMech(GameObject mechObject)
    {
        PrepareInterp();
        offsetPos = -10 * mechObject.transform.forward + 5 * mechObject.transform.up; // Forward multiplier determines distance behind, up for above
        offsetRot = Quaternion.Euler(30, 0, 0); // X value determines level of downward tilt
        targetObject = mechObject;
    }

    public void AttachToWeapon(GameObject weaponObject)
    {
        PrepareInterp();
        offsetPos = Vector3.zero;
        offsetRot = Quaternion.identity;
        targetObject = weaponObject;
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
    }

    private void InterpolatePosition()
    {
        transform.position = Vector3.Lerp(originalPos, targetObject.transform.position + offsetPos, ComputeArcLengthFromTime(transitionTime));
        transform.rotation = Quaternion.Slerp(originalRot, targetObject.transform.rotation * offsetRot, ComputeArcLengthFromTime(transitionTime));

        if (transitionTime >= transitionMaxTime)
        {
            interpolating = false;
            transform.parent = targetObject.transform;
            playerInput.FinalizeInterp(targetObject);
        }

        transitionTime += DT;
    }

    private float ComputeArcLengthFromTime(float time)
    {
        float normalizedTime = time / transitionMaxTime;
        return -2 * (float)Math.Pow(normalizedTime, 3) + 3 * (float)Math.Pow(normalizedTime, 2);
    }
}
