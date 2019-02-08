using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(LineRenderer))]

public class PathPredictor : NetworkBehaviour
{
    LineRenderer lr;
    private int moveSpeed;
    private int rotateSpeed;
    public int pathLength = 6000;
    GameObject future;
    private float nextActionTime = 0.0f;
    public float period = 1f;
    public GameObject mech;
    MechMovement MM;

    // Start is called before the first frame update
    void Start()
    {
        if (!mech)
        {
            mech = gameObject;
        }
        lr = mech.GetComponent<LineRenderer>();

        MM = GameObject.Find("InputManager").GetComponent<MechMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!MM)
        {
            MechMovement MM = GameObject.Find("InputManager").GetComponent<MechMovement>();
            mech = MM.mech;
            return;
        }

        rotateSpeed = MM.rotateSpeed;
        moveSpeed = MM.moveSpeed;

        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            RenderPath();
        }

        mech.transform.Translate(0, 0, Time.deltaTime * moveSpeed, Space.Self);
        mech.transform.Rotate(0, Time.deltaTime * rotateSpeed, 0, Space.Self);
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
        future.transform.position = mech.transform.position;
        future.transform.rotation = mech.transform.rotation;

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
