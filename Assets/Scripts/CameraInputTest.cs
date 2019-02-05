using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInputTest : MonoBehaviour
{

    private CameraManager camManager;
    private GameObject mech1, mech2;

    // Start is called before the first frame update
    void Start()
    {
        camManager = GameObject.Find("Main Camera").GetComponent<CameraManager>();
        mech1 = GameObject.Find("MechTesting");
        mech2 = GameObject.Find("MechTesting (1)");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            camManager.FollowMech(mech1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            camManager.FollowMech(mech2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            camManager.ResetPosition();
        }
    }
}
