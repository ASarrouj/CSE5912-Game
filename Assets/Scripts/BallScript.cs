using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool startLeft;
    Vector3 vel;
    void Start()
    {
        startLeft=true;
        vel=new Vector3(-0.1f,0,-.1f);
    }

    // Update is called once per frame
    void Update()
    {
    transform.Translate(vel);
        if(transform.position.x>10f||transform.position.x<-10f)
        {
            transform.position=new Vector3(0f,0.5f,0f);
        }
        if(transform.position.z>5f||transform.position.z<-5f)
        {
            vel=new Vector3(vel.z,0,-vel.x);
        }
        
    }
}
