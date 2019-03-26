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
            SetVictoryText("Round Loss");
            lobbyManager.CheckRoundOver();
        }

        public void SetVictoryText(string text)
        {
            victoryText.GetComponent<Text>().text = text;
        }

        public void SetTextActive(bool active)
        {
            victoryText.SetActive(active);
        }
    }
}
