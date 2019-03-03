using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnAI() {
        GameObject spawner = GameObject.Find("AI_Spawner");
        if (spawner != null) {
            spawner.GetComponent<AI_Spawner>().SpawnAI();
        }
    }
}
