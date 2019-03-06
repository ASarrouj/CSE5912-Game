using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    // Start is called before the first frame update
    void Start()
    {
        bar = transform;
    }

    public void setHealthBar(float health) {
        transform.localScale = new Vector3(health, 1f);
    }

}
