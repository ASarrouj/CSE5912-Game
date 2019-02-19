using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxChecker : MonoBehaviour
{
    public enum Hitbox {FrontHitbox, LeftHitbox, RightHitbox}
    public Hitbox hitboxType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
         if (hitboxType == Hitbox.FrontHitbox)
        {
            Debug.Log("AA");
        }
    }

}
