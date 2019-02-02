using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private Vector3 offsetPos;
    private Quaternion offsetRot;
    private float transitionTime;
    private const float transitionMaxTime = 4.0f;
    private const float DT = 0.01f;
    private bool interpolating;
    private Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        interpolating = false;
        transitionTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (interpolating)
            InterpolatePosition();
    }

    public void FollowMech(Transform mechTransform)
    {
        PrepareInterp();
        offsetPos = -5 * mechTransform.forward + 3 * mechTransform.up; // Forward multiplier determines distance behind, up for above
        offsetRot = Quaternion.Euler(30, 0, 0); // X value determines level of downward tilt
        targetTransform = mechTransform;
    }

    public void AttachToWeapon(Transform weaponTransform)
    {
        PrepareInterp();
        offsetPos = Vector3.zero;
        offsetRot = Quaternion.identity;
        targetTransform = weaponTransform;
    }

    public void ResetPosition()
    {
        PrepareInterp();
        offsetPos = Vector3.zero;
        offsetRot = Quaternion.identity;
        targetTransform = GameObject.Find("DebugCameraPos").transform;
    }

    private void PrepareInterp()
    {
        interpolating = true;
        transitionTime = 0.0f;
        transform.parent = null;
    }

    private void InterpolatePosition()
    {
        transform.position = Vector3.Lerp(transform.position, targetTransform.position + offsetPos, ComputeArcLengthFromTime(transitionTime));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetTransform.rotation * offsetRot, ComputeArcLengthFromTime(transitionTime));

        if (transitionTime == transitionMaxTime)
        {
            interpolating = false;
            transform.parent = targetTransform;
        }

        transitionTime += DT;
    }

    private float ComputeArcLengthFromTime(float time)
    {
        float normalizedTime = time / transitionMaxTime;
        return -2 * Pow(normalizedTime, 3) + 3 * Pow(normalizedTime, 2);
    }

    private float Pow(float value, int exponent)
    {
        while (exponent > 1)
        {
            value *= value;
            exponent--;
        }
        return value;
    }
}
