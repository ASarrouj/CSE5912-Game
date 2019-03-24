using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechDriver : MonoBehaviour
{
    private const float wheelTorque = 1000;
    private float maxVelocity = 30;
    private float velLimit;
    private const float velDelta = 5;

    private float turnAngle;
    private const float maxTurnAngle = 30;
    private const float turnDelta = 0.25f;
    private float maxEngineVolume;

    private WheelCollider frontLeftCollider, frontRightCollider;
    private List<WheelCollider> allColliders;
    private Transform frontLeftWheel, frontRightWheel;
    private List<Transform> allWheels;
    private Rigidbody rb;
    private AudioSource engineSound;

    void Start()
    {
        turnAngle = 0;
        velLimit = 0;

        allColliders = new List<WheelCollider>();
        allWheels = new List<Transform>();
        rb = GetComponent<Rigidbody>();
        engineSound = GetComponent<AudioSource>();
        maxEngineVolume = engineSound.volume;

        Transform tiresParent = transform.Find("NewMechWithGuns").Find("Tires");
        for (int i = 0; i < tiresParent.childCount; i++)
        {
            allWheels.Add(tiresParent.GetChild(i).GetChild(0));
        }
        frontRightWheel = allWheels[0];
        frontLeftWheel = allWheels[3];

        Transform colliderParent = transform.Find("NewMechWithGuns").Find("TireColliders");
        for (int i = 0; i < tiresParent.childCount; i++)
        {
            allColliders.Add(colliderParent.GetChild(i).GetComponent<WheelCollider>());
        }
        frontRightCollider = allColliders[0];
        frontLeftCollider = allColliders[3];
    }

    void FixedUpdate()
    {
        foreach (WheelCollider collider in allColliders)
        {
            if (rb.velocity.magnitude <= Mathf.Abs(velLimit))
            {
                collider.motorTorque = Mathf.Sign(velLimit) * wheelTorque;
            }
            else
            {
                rb.velocity = rb.velocity.normalized * Mathf.Abs(velLimit);
            }
        }

        if (rb.velocity.magnitude == 0)
        {
            turnAngle = 0;
        }

        engineSound.volume = (rb.velocity.magnitude / maxVelocity) * maxEngineVolume;

        frontLeftCollider.steerAngle = turnAngle;
        frontRightCollider.steerAngle = turnAngle;
        Debug.Log(rb.velocity.magnitude * Mathf.Sign(velLimit));

        for (int i = 0; i < allColliders.Count; i++)
        {
            UpdateWheelPositions(allColliders[i], allWheels[i]);
        }
    }

    public void TurnLeft()
    {
        if (turnAngle >= -maxTurnAngle)
        {
            turnAngle -= turnDelta;
        }
    }

    public void TurnRight()
    {
        if (turnAngle <= maxTurnAngle)
        {
            turnAngle += turnDelta;
        }
    }

    public void Accelerate()
    {
        if (velLimit < maxVelocity)
        {
            velLimit += velDelta;
        }
    }

    public void Decelerate()
    {
        if (velLimit > -maxVelocity)
        {
            velLimit -= velDelta;
        }
    }

    private void UpdateWheelPositions(WheelCollider collider, Transform wheel)
    {
        Vector3 pos = wheel.position;
        Quaternion rot = wheel.rotation;

        collider.GetWorldPose(out pos, out rot);

        wheel.position = pos;
        wheel.rotation = rot * Quaternion.Euler(0, 90, 0);
    }
}
