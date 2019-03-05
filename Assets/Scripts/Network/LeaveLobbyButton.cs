using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LeaveLobbyButton : MonoBehaviour
{
    public GameObject manager;

    public void LeaveLobby()
    {
        NetworkManager.Shutdown();
        GameStateManager.QuickLoadState(2);
        Destroy(manager);
    }
}
