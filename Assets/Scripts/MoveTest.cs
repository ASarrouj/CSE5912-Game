﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (LineRenderer))]
public class MoveTest : MonoBehaviour
{
    LineRenderer lr;
    public int moveSpeed;
    public int rotateSpeed;
    public int pathLength = 6000;
    GameObject future;
    private float nextActionTime = 0.0f;
    public float period = 1f;

    private void Awake()
    {
       

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