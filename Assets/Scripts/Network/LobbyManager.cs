using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyManager : NetworkLobbyManager
{
    public void CreateMatch()
    {
        singleton.StartHost();
    }
}

