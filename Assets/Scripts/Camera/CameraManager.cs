﻿using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private Vector3 offsetPos, originalPos;
    private Quaternion offsetRot, originalRot;
    private float transitionTime, dragSpeed, dragOrigin, lastMousePosX;
    private const float transitionMaxTime = 0.2f;
    private const float DT = 0.005f;
    private bool interpolating, interpolatingBuilder;
    private Transform target;
    private PlayerInput playerInput;
    private int joystickLookSpeed = 5;
    private Vector3[] customOffsetPoses;
    private Quaternion[] customOffsetRots;
    private int buildSlotIndex;

    // Start is called before the first frame update
    void Start()
    {
        transitionTime = 0.0f;
        dragSpeed = 0.2f;
        lastMousePosX = Screen.width / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (interpolating)
            InterpolateCamera();
        else if (interpolatingBuilder)
            InterpolateCameraBuilder();
    }

    public void SetInput(PlayerInput input)
    {
        playerInput = input;
    }

    public void SetBuilderLocations(Vector3[] poses, Quaternion[] rots)
    {
        customOffsetPoses = poses;
        customOffsetRots = rots;
    }

    public void FollowMech(Transform player)
    {
        offsetPos = new Vector3(-10.5f, 4f, -2); // X multiplier determines distance behind, Y for above, Z for right
        offsetRot = Quaternion.Euler(25, 0, 0); // X value determines level of downward tilt
        if (target != player) // If target is already the mech, skip the interpolation
        {
            PrepareInterp();
            target = player;
        }
        else
        {
            SkipInterp();
        }
    }

    public void MoveToNextBuildSlot(Transform player)
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
        target = player;
        transitionTime = 0.0f;
        offsetPos = customOffsetPoses[buildSlotIndex];
        offsetRot = customOffsetRots[buildSlotIndex];
        interpolatingBuilder = true;
        buildSlotIndex++;
        Debug.Log(buildSlotIndex);
    }

    public void AttachToWeapon(Transform weapon)
    {
        PrepareInterp();
        offsetPos = new Vector3(0, 1, 0); // X multiplier determines distance behind, Y for above
        offsetRot = Quaternion.identity;
        target = weapon;
    }

    public void ResetPosition()
    {
        PrepareInterp();
        offsetPos = Vector3.zero;
        offsetRot = Quaternion.identity;
        target = GameObject.Find("DebugCameraPos").transform;
    }

    private void PrepareInterp()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
        interpolating = true;
        transitionTime = 0.0f;
        transform.parent = null;
    }

    private void SkipInterp()
    {
        playerInput.SetInputState();
    }

    private void InterpolateCamera()
    {
        transform.position = Vector3.Lerp(originalPos, target.position + (offsetPos.x * target.forward + offsetPos.y * target.up + offsetPos.z * target.right), ComputeArcLengthFromTime(transitionTime));
        transform.rotation = Quaternion.Slerp(originalRot, target.rotation * offsetRot, ComputeArcLengthFromTime(transitionTime));

        if (transitionTime >= transitionMaxTime)
        {
            interpolating = false;
            transform.parent = target;
            playerInput.SetInputState();
        }

        transitionTime += DT;
    }

    private void InterpolateCameraBuilder()
    {
        transform.position = Vector3.Lerp(originalPos, target.position + (offsetPos.x * target.forward + offsetPos.y * target.up + offsetPos.z * target.right), ComputeArcLengthFromTime(transitionTime));
        transform.rotation = Quaternion.Slerp(originalRot, target.rotation * offsetRot, ComputeArcLengthFromTime(transitionTime));

        if (transitionTime >= transitionMaxTime)
        {
            interpolatingBuilder = false;
            target = null;
        }

        transitionTime += DT;
    }

    private float ComputeArcLengthFromTime(float time)
    {
        float normalizedTime = time / transitionMaxTime;
        return -2 * (float)Math.Pow(normalizedTime, 3) + 3 * (float)Math.Pow(normalizedTime, 2);
    }

    public void DragCamera()
    {
        float dragDistance = (Input.mousePosition.x - dragOrigin - lastMousePosX) * dragSpeed;

        //if (Mathf.Abs(Input.mousePosition.x - dragOrigin) < Screen.width / 4)
        //{
            transform.RotateAround(target.position, target.up, dragDistance);
        //}

        lastMousePosX = Input.mousePosition.x - dragOrigin;
    }

    public void UnDragCamera()
    {
        transform.position = target.position + (offsetPos.x * target.forward + offsetPos.y * target.up + offsetPos.z * target.right);
        transform.rotation = target.rotation * offsetRot;
    }

    public void SetDragOrigin()
    {
        dragOrigin = Input.mousePosition.x;
        lastMousePosX = 0;
    }

    public void RotateCamera(float xInput) {
        transform.RotateAround(target.position, target.up, xInput * joystickLookSpeed);
    }

    public Ray CreateRayFromMouseClick()
    {
        return GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
    }
}
