using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MechMovement : NetworkBehaviour
{
    private int moveSpeed;
    private int rotateSpeed;
    public GameObject mech;

    // Start is called before the first frame update
    void Start()
    {
        if (!mech)
        {
            mech = gameObject;
        }
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
        mech.transform.Translate(0, 0, Time.deltaTime * moveSpeed, Space.Self);
        mech.transform.Rotate(0, Time.deltaTime * rotateSpeed, 0, Space.Self);
    }
}
