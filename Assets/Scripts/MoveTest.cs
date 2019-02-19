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
    private float nextActionTime = 0.0f;
    public float period = 0.1f;
    private Vector3 com;
    private Rigidbody rb;

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
        moveSpeed = 0;
        rotateSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            RenderPath();
        }
        
        transform.Translate(0, 0, Time.deltaTime * moveSpeed, Space.Self);
        transform.Rotate(0, Time.deltaTime * rotateSpeed, 0, Space.Self);

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
