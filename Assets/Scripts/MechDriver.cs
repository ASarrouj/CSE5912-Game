using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
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

    private LineRenderer lr;
    public int pathPredictorLength = 600;
    private GameObject future;

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

        lr = GetComponent<LineRenderer>();
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
        frontLeftWheel.rotation = Quaternion.Euler(frontLeftWheel.localRotation.eulerAngles.x, frontLeftWheel.localRotation.eulerAngles.y, turnAngle);
        frontRightWheel.rotation = Quaternion.Euler(frontRightWheel.localRotation.eulerAngles.x, frontRightWheel.localRotation.eulerAngles.y, turnAngle);
        //Debug.Log(rb.velocity.magnitude * Mathf.Sign(velLimit));

        for (int i = 0; i < allColliders.Count; i++)
        {
            UpdateWheelPositions(allColliders[i], allWheels[i]);
        }

        RenderPath();
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

    public void ResetRotation()
    {
        while (transform.rotation.z > 0) {
            transform.Rotate(0, 0, -1 * Time.deltaTime);
        }
        while (transform.rotation.z < 0)
        {
            transform.Rotate(0, 0, 1 * Time.deltaTime);
        }
        while (transform.rotation.x > 0)
        {
            transform.Rotate(-1 * Time.deltaTime, 0, 0);
        }
        while (transform.rotation.x > 0)
        {
            transform.Rotate(1 * Time.deltaTime, 0, 0);
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

    private void RenderPath()
    {
        lr.SetVertexCount(pathPredictorLength);
        lr.SetPositions(CalculatePathArray());
    }

    private Vector3[] CalculatePathArray()
    {
        Vector3[] PathArray = new Vector3[pathPredictorLength];

        future = new GameObject();
        future.transform.position = transform.position;
        future.transform.rotation = transform.rotation;

        for (int i = 0; i < pathPredictorLength; i++)
        {
            PathArray[i] = CalculatePathPoint(i);
        }

        Destroy(future);
        return PathArray;
    }

    private Vector3 CalculatePathPoint(int frame)
    {
        if (velLimit >= 0)
        {
            future.transform.Rotate(0, Time.deltaTime * turnAngle, 0, Space.Self);
        } else { 
            future.transform.Rotate(0, -(Time.deltaTime * turnAngle), 0, Space.Self);
        }
        future.transform.Translate(0, 0, Time.deltaTime * velLimit, Space.Self);

        Vector3 futurePos = future.transform.position;
        return futurePos;
    }
}
