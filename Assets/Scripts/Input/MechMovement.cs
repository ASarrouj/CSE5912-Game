using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechMovement : MonoBehaviour
{
    public int moveSpeed;
    public int rotateSpeed;
    public GameObject mech;
    public bool takeInput;

    // Start is called before the first frame update
    void Start()
    {   
        moveSpeed = 0;
        rotateSpeed = 0;
        takeInput = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mech)
        {
            return;
        }

        if (takeInput)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && moveSpeed < 3)
            {
                moveSpeed++;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && moveSpeed > -1)
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
        }

        if (moveSpeed == 0)
        {
            rotateSpeed = 0;
        }

        mech.transform.Translate(0, 0, Time.deltaTime * moveSpeed, Space.Self);
        mech.transform.Rotate(0, Time.deltaTime * rotateSpeed, 0, Space.Self);
    }
}
