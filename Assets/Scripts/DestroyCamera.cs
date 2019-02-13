using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<Camera>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
