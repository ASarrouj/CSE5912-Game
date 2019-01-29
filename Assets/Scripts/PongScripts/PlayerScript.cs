using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private bool pause;
    public Light spot;
    // Start is called before the first frame update
    void Start()
    {
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        spot.transform.position=transform.position+new Vector3(0,10f,0);
        if (!pause)
        {
            if (Input.GetKey(KeyCode.S) && transform.position.z > -5f)
            {
                transform.Translate(0, 0, -.15f);
            }
            else if (Input.GetKey(KeyCode.W) && transform.position.z < 5f)
            {
                transform.Translate(0, 0, .15f);
            }
        }
    }

    public void Halt()
    {
        pause = true;
    }

    public void Resume()
    {
        pause = false;
    }
}
