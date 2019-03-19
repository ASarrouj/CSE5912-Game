using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (LineRenderer))]
public class MoveTest : MonoBehaviour
{
    LineRenderer lr;
    public int moveSpeed;
    public float rotateSpeed;
    public int pathLength = 600;
    GameObject future;
    private Vector3 com;
    private Rigidbody rb;
    private AudioSource engineSound;
    private List<WheelCollider> wheelColliders;
    private List<Transform> wheels;

    private void Awake()
    {
        com = new Vector3(0,-1,0);
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com;
        Physics.gravity = new Vector3(0, -10f, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        engineSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RenderPath();

        if (moveSpeed != 0)
        {
            engineSound.enabled = true;
        }
        else
        {
            engineSound.enabled = false;
        }

    }

    void RenderPath()
    {
        lr.SetVertexCount(pathLength);
        lr.SetPositions(CalculatePathArray());
    }

    Vector3[] CalculatePathArray()
    {
        Vector3[] PathArray = new Vector3[pathLength];

        future = new GameObject();
        future.transform.position = transform.position;
        future.transform.rotation = transform.rotation;

        for (int i = 0; i < pathLength; i++)
        {
            PathArray[i] = CalculatePathPoint(i);
        }

        Destroy(future);
        return PathArray;
    }

    Vector3 CalculatePathPoint(int frame)
    {
       future.transform.Rotate(0, Time.deltaTime * rotateSpeed, 0, Space.Self);
       future.transform.Translate(0, 0, Time.deltaTime * moveSpeed, Space.Self);
       Vector3 futurePos = future.transform.position;
       return futurePos;
    }
}
