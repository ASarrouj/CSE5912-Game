using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.K)&&transform.position.z>-5f)
        {
            transform.Translate(0,0,-.1f);
        }
        else if(Input.GetKey(KeyCode.I)&&transform.position.z<5f)
        {
            transform.Translate(0,0,.1f);
        }
    }
}
