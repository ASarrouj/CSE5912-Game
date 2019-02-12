﻿using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private Vector3 offsetPos, originalPos;
    private Quaternion offsetRot, originalRot;
    private float transitionTime;
    private const float transitionMaxTime = 0.5f;
    private const float DT = 0.005f;
    private bool interpolating;
    private Transform target;
    private UIManager uiManager;
    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
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

    public void FollowMech(Transform mech)
    {
        PrepareInterp();
        offsetPos = -10 * mech.forward + 5 * mech.up; // Forward multiplier determines distance behind, up for above
        offsetRot = Quaternion.Euler(30, 0, 0); // X value determines level of downward tilt
        target = mech;
    }

    public void AttachToWeapon(Transform weapon)
    {
        PrepareInterp();
        offsetPos = Vector3.zero;
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

    private void InterpolatePosition()
    {
        transform.position = Vector3.Lerp(originalPos, target.position + offsetPos, ComputeArcLengthFromTime(transitionTime));
        transform.rotation = Quaternion.Slerp(originalRot, target.rotation * offsetRot, ComputeArcLengthFromTime(transitionTime));

        if (transitionTime >= transitionMaxTime)
        {
            interpolating = false;
            transform.parent = target.transform;
            playerInput.EnableInput(target);
        }

        transitionTime += DT;
    }

    private float ComputeArcLengthFromTime(float time)
    {
        float normalizedTime = time / transitionMaxTime;
        return -2 * (float)Math.Pow(normalizedTime, 3) + 3 * (float)Math.Pow(normalizedTime, 2);
    }
}
