using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Light spot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spot.transform.position=transform.position+new Vector3(0,30,0);
        if(Input.GetKey(KeyCode.S)&&transform.position.z>-5f)
        {
            transform.Translate(0,0,-.15f);
        }
        else if(Input.GetKey(KeyCode.W)&&transform.position.z<5f)
        {
            transform.Translate(0,0,.15f);
        }
    }
}
