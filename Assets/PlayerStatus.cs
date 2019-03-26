using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStatus : NetworkBehaviour
{
    public bool Destroyed { get; set; }

    private void Start()
    {
        Destroyed = false;
    }
}
