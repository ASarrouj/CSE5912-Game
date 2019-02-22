using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StandaloneServerAutostart : MonoBehaviour
{
    bool started;
    NetworkManager netManager;

    // Start is called before the first frame update
    void Start()
    {
        started = false;
        netManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_SERVER
        if (!started)
        {
            Debug.Log("Server build, hosting game now.");
            started = true;
            netManager.StartHost();    
        }
#endif
    }
}
