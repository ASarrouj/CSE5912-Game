using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Prototype.NetworkLobby
{
    public class LobbyMatchSettings : MonoBehaviour
    {
        public LobbyManager lobbyManager;
        public GameObject mapDropdown;

        public void OnSelectMap()
        {
            lobbyManager.playScene = mapDropdown.GetComponentInChildren<Text>().text;
        }
    }
}
