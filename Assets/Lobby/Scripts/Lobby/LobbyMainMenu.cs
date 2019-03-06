using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.NetworkLobby
{
    public class LobbyMainMenu : MonoBehaviour
    {
        public LobbyManager lobbyManager;

        public void OnClickPlay()
        {
            lobbyManager.ChangeTo(lobbyManager.gamesPanel);
        }
    }
}
