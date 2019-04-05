using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryRotation
{
    // Start is called before the first frame update
    public Transform weaponBase;
    Quaternion originalRotation;
    float sensitive=1.0f;
        public ArtilleryRotation(Transform weapon)
    {
        //this.weapon = weapon;
        /*Rigidbody rb = weapon.GetComponent<Rigidbody>();
        if (rb)
            rb.freezeRotation = true;
        originalRotation = weapon.localRotation;*/
    }
    public void Update(PlayerInput.InputType inputType)
    {
        //weaponBase.Rotate();
    }
}
