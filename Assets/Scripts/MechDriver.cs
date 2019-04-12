using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MechDriver : MonoBehaviour
{
    private const float wheelTorque = 1000;
    private float maxVelocity = 10;
    public float velLimit;
    private const float velDelta = 2;

    public float turnAngle;
    private const float maxTurnAngle = 25;
    private const float turnDelta = 0.5f;
    private float maxEngineVolume;

    private WheelCollider frontLeftCollider, frontRightCollider;
    private List<WheelCollider> allColliders;
    private Transform frontLeftWheel, frontRightWheel;
    private List<Transform> allWheels;
    private Rigidbody rb;
    private AudioSource[] mechSounds;

    private LineRenderer lr;
    public int pathPredictorLength = 600;
    public float thrust = 150000;
    public bool canJump = false;
    public bool canShield = false;
    private GameObject future;
    private float nextJump;
    private float jumpCoolDown = 5.0f;

    private void Awake() {
        allColliders = new List<WheelCollider>();
        allWheels = new List<Transform>();
    }

    void Start()
    {
        turnAngle = 0;
        velLimit = 0;

        rb = GetComponent<Rigidbody>();
        mechSounds = GetComponents<AudioSource>();
        maxEngineVolume = mechSounds[0].volume;

        rb.centerOfMass = transform.Find("CenterOfMass").transform.localPosition;

        lr = GetComponent<LineRenderer>();
    }

    void FixedUpdate()
    {
        foreach (WheelCollider collider in allColliders)
        {
            if (rb.velocity.magnitude < Mathf.Abs(velLimit))
            {
                collider.motorTorque = Mathf.Sign(velLimit) * wheelTorque;
            }
            else
            {
                rb.velocity = rb.velocity.normalized * Mathf.Abs(velLimit);
            }
        }

        if (rb.velocity.magnitude <= 0.5f && rb.velocity.magnitude >= -0.5f && (velLimit >= 5 || velLimit <= -5))
        {
            velLimit = Mathf.Sign(velLimit) * 5;
        }

        mechSounds[0].volume = (rb.velocity.magnitude / maxVelocity) * maxEngineVolume;

        frontLeftCollider.steerAngle = turnAngle;
        frontRightCollider.steerAngle = turnAngle;

        for (int i = 0; i < allColliders.Count; i++)
        {
            UpdateWheelPositions(allColliders[i], allWheels[i]);
        }

        RenderPath();

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            transform.Translate(0, 5, 0);
            transform.localRotation = Quaternion.identity;
        }
    }

    public void findColliders()
    {
        Transform tiresParent = transform.Find("NewMechWithSlots").Find("Tires");
        for (int i = 0; i < tiresParent.childCount; i++)
        {
            allWheels.Add(tiresParent.GetChild(i).GetChild(0));
        }
        frontRightWheel = allWheels[0];
        frontLeftWheel = allWheels[3];
        Transform colliderParent = transform.Find("NewMechWithSlots").Find("TireColliders");
        for (int i = 0; i < tiresParent.childCount; i++)
        {
            allColliders.Add(colliderParent.GetChild(i).GetComponent<WheelCollider>());
        }
        frontRightCollider = allColliders[0];
        frontLeftCollider = allColliders[3];
        GetComponent<Rigidbody>().useGravity = true;
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

    public void jump()
    {
        if (canJump && Time.time > nextJump) 
        {
            nextJump = Time.time + jumpCoolDown;
            rb.AddRelativeForce(new Vector3(0, 1, 9) * thrust, ForceMode.Impulse);  
        }
    }

    public void shield() {
        if (canShield) { GetComponent<DamageOverNetwork>().turnInvincible(); }
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Building")
        {
            mechSounds[1].Play();
        }
    }
}
