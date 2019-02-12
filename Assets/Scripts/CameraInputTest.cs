using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInputTest : MonoBehaviour
{

    private CameraManager camManager;
    private UIManager UIMan;
    private MechMovement MM;
    private GameObject mainCam;
    private Camera currentCam;
    private Transform mech;

    // Start is called before the first frame update
    void Start()
    {
        camManager = GameObject.Find("Main Camera").GetComponent<CameraManager>();
        UIMan = GameObject.Find("UI").GetComponent<UIManager>();
        MM = GameObject.Find("InputManager").GetComponent<MechMovement>();
        mainCam = GameObject.Find("Main Camera");
        currentCam = mainCam.GetComponent<Camera>();
        mech = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentCam.enabled = false;
            currentCam = mainCam.GetComponent<Camera>();
            currentCam.enabled = true;

            UIMan.navigationUI();
            //MM.mech = mech;
            MM.takeInput = true;
            mech.GetComponent<LineRenderer>().enabled = true;
            mech.GetComponent<PathPredictor>().enabled = true;
            camManager.FollowMech(mech);
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentCam != mainCam)
            {
                currentCam.enabled = false;
                currentCam = mainCam.GetComponent<Camera>();
                currentCam.enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                MM.takeInput = false;
                UIMan.noUI();
                if (currentCam != mainCam)
                {
                    currentCam.enabled = false;
                    currentCam = mainCam.GetComponent<Camera>();
                    currentCam.enabled = true;
                }
                mech.GetComponent<PathPredictor>().enabled = false;
                mech.GetComponent<LineRenderer>().enabled = false;

                camManager.ResetPosition();
            }

            MM.takeInput = false;
            mech.GetComponent<PathPredictor>().enabled = false;
            mech.GetComponent<LineRenderer>().enabled = false;

            GameObject f = mech.transform.Find("LeftGun").gameObject;
            GameObject mg = f.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            GameObject gunCam = mg.transform.Find("GunCamera").gameObject;

            UIMan.gunUI();
            //camManager.AttachToWeapon(gunCam);
        }
    }

    void weaponCameraCallback()
    {
        GameObject f = mech.transform.Find("LeftGun").gameObject;
        GameObject mg = f.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        GameObject gunCam = mg.transform.Find("GunCamera").gameObject;

        currentCam.enabled = false;
        currentCam = gunCam.GetComponent<Camera>();
        currentCam.enabled = true;
    }
}
