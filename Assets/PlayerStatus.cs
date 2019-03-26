using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Prototype.NetworkLobby
{
    public class PlayerStatus : NetworkBehaviour
    {
        private bool destroyed;
        private LobbyManager lobbyManager;

        private void Awake()
        {
            lobbyManager = LobbyManager.s_Singleton;
        }

        public bool Destroyed
        {
            get { return destroyed; }
            set
            {
                destroyed = value;
                OnDestroyed();
            }
        }

        private void Start()
        {
            Destroyed = false;
        }

        public void OnDestroyed()
        {
            lobbyManager.CheckWinStatus();
        }
    }
}
