using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Prototype.NetworkLobby
{
    public class PlayerStatus : NetworkBehaviour
    {
        public GameObject victoryText;

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
            lobbyManager.CheckRoundOver();
        }

        [ClientRpc]
        public void RpcSetVictoryText(string text)
        {
            victoryText.GetComponent<Text>().text = text;
        }

        [ClientRpc]
        public void RpcSetTextActive(bool active)
        {
            victoryText.SetActive(active);
        }
    }
}
