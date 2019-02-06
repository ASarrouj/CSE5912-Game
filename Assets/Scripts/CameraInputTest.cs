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
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] mechs = GameObject.FindGameObjectsWithTag("Mech");

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            camManager.FollowMech(mechs[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            camManager.FollowMech(mechs[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            camManager.ResetPosition();
        }
    }
}
