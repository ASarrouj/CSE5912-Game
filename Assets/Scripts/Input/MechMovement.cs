﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(LineRenderer))]

public class MechMovement : NetworkBehaviour
{
    LineRenderer lr;
    private int moveSpeed;
    private int rotateSpeed;
    public int pathLength = 6000;
    GameObject future;
    private float nextActionTime = 0.0f;
    public float period = 1f;
    public GameObject mech;

    // Start is called before the first frame update
    void Start()
    {
        if (!mech)
        {
            mech = gameObject;
        }
        lr = mech.GetComponent<LineRenderer>();
        moveSpeed = 0;
        rotateSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && moveSpeed<3)
        {
            moveSpeed++;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && moveSpeed >-1)
        {
            moveSpeed--;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && moveSpeed != 0 && rotateSpeed >= -30 && rotateSpeed <= 30)
        {
            if (moveSpeed > 0 && rotateSpeed > -30)
            {
                rotateSpeed -= 5;
            }
            else if (moveSpeed < 0 && rotateSpeed < 30)
            {
                rotateSpeed += 5;
            }

        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && moveSpeed != 0 && rotateSpeed <= 30 && rotateSpeed >= -30)
        {
            if (moveSpeed > 0 && rotateSpeed < 30)
            {
                rotateSpeed += 5;
            }
            else if (moveSpeed < 0 && rotateSpeed > -30)
            {
                rotateSpeed -= 5;
            }
        }
        if (moveSpeed == 0)
        {
            rotateSpeed = 0;
        }

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
