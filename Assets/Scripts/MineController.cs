using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{

    private bool armed;
    void Start()
    {
        armed = false;
        Invoke("ArmMine", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" && armed == true)
        {
            Debug.Log("Mine Explode");
        }
    }

    private void ArmMine()
    {
        armed = true;
    }
}
