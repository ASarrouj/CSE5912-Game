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
    private GameObject mech;

    // Start is called before the first frame update
    void Start()
    {
        camManager = GameObject.Find("Main Camera").GetComponent<CameraManager>();
        UIMan = GameObject.Find("UI").GetComponent<UIManager>();
        MM = GameObject.Find("InputManager").GetComponent<MechMovement>();
        mainCam = GameObject.Find("Main Camera");
        currentCam = mainCam.GetComponent<Camera>();
        mech = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mech)
        {
            GameObject[] mechs = GameObject.FindGameObjectsWithTag("Mech");
            if(mechs.Length == 1)
            {
                mech = mechs[0];
            }
            else if (mechs == null && mechs.Length == 2)
            {
                mech = mechs[1];
            }
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (currentCam != mainCam)
            {
                currentCam.enabled = false;
                currentCam = mainCam.GetComponent<Camera>();
                currentCam.enabled = true;
            }

            UIMan.navigationUI();
            MM.mech = mech;
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

            MM.takeInput = false;
            mech.GetComponent<PathPredictor>().enabled = false;
            mech.GetComponent<LineRenderer>().enabled = false;

            GameObject f = mech.transform.Find("LeftGun").gameObject;
            GameObject mg = f.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
            GameObject gunCam = mg.transform.Find("GunCamera").gameObject;

            UIMan.gunUI();
            camManager.AttachToWeapon(gunCam, weaponCameraCallback);
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
